using AutoMapper;
using EpubSharp;
using FictionHoarderWPF.Core;
using FictionHoarderWPF.MVVM.Model;
using FictionUI_Library.API;
using FictionUI_Library.EventAggregators;
using HTMLConverter;
using Prism.Events;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Markup;
using System.Xml;

namespace FictionHoarderWPF.MVVM.ViewModel
{
    class ReadPageModel : ObservableObject
    {
        #region ----------Fields----------

        private readonly IMapper _mapper;
        private readonly IApiHelper _apiHelper;
        private readonly IStoryEndpoint _storyEndpoint;
        private readonly IEventAggregator _eventAggregator;
        private StoryDisplayModel _storyInfo;
        private EpubBook _book;
        private FlowDocument _storyDocument;
        private Visibility _spinnerVisivility;
        private ICommand _goToHomeCommand;
        private ICommand _changeChapterCommand;
        private ICommand _toggleMenuCommand;
        private List<string> _chapterTitles;
        private int _chapterNumber = 0;
        private int _selectedChapterIndex = 0;
        private bool _isburgerMenuOpen;

        #endregion

        public event EventHandler ChangeChapterFromMenu;

        public ReadPageModel(IMapper mapper, IApiHelper apiHelper, IStoryEndpoint storyEndpoint, 
            IEventAggregator eventAggregator)
        {
            _mapper = mapper;
            _apiHelper = apiHelper;
            _storyEndpoint = storyEndpoint;
            _eventAggregator = eventAggregator;

            _eventAggregator.GetEvent<StorySelectionEvent>().Subscribe((story) =>
            {
                StoryInfo = _mapper.Map<StoryDisplayModel>(story);
                SetBook();
            });

            ChangeChapterFromMenu += SelectChapter;

            ChapterTitles = new List<string>();
            IsBurgerMenuOpen = false;
        }

        #region -----------Properties----------

        public StoryDisplayModel StoryInfo
        {
            get { return _storyInfo; }
            set
            {
                _storyInfo = value;
            }
        }

        public FlowDocument StoryDocument
        {
            get { return _storyDocument; }
            set 
            { 
                _storyDocument = value; 
                OnPropertyChanged(nameof(StoryDocument));
            }
        }

        public string ChapterHeader
        {
            get { return $"Page {ChapterNumber}"; }
        }

        public List<string> ChapterTitles 
        { 
            get { return _chapterTitles; }
            set
            {
                _chapterTitles = value;
                OnPropertyChanged(nameof(ChapterTitles));
            }
        }

        public bool IsBurgerMenuOpen 
        { 
            get { return _isburgerMenuOpen; }
            set
            {
                _isburgerMenuOpen = value;
                OnPropertyChanged(nameof(IsBurgerMenuOpen));
            }
        }

        public int ChapterNumber
        {
            get { return _chapterNumber; }
            set
            {
                _chapterNumber = value;
                OnPropertyChanged(nameof(ChapterNumber));
                OnPropertyChanged(nameof(ChapterHeader));
            }
        }

        public int SelectedChapterIndex 
        { 
            get { return _selectedChapterIndex; }
            set
            {
                _selectedChapterIndex = value;
                OnPropertyChanged(nameof(SelectedChapterIndex));
                ChangeChapterFromMenu.Invoke(this, new EventArgs());
            } 
        }

        public Visibility SpinnerVisibility
        {
            get { return _spinnerVisivility; }
            set
            {
                _spinnerVisivility = value;
                OnPropertyChanged(nameof(SpinnerVisibility));
            }
        }

        public ICommand GoToHomeCommand
        {
            get 
            {
                if (_goToHomeCommand is null)
                {
                    _goToHomeCommand = new RelayCommand(p => ChangeViewModel((ObservableObject)p), p => p is ObservableObject);
                }

                return _goToHomeCommand; 
            }
        }

        public ICommand ChangeChapterCommand
        {
            get
            {
                if (_changeChapterCommand is null)
                {
                    _changeChapterCommand = new RelayCommand(p => ChangeChapter((string)p), p => true);
                }

                return _changeChapterCommand;
            }
        }

        public ICommand ToggleMenuCommand
        {
            get
            {
                if (_toggleMenuCommand is null)
                {
                    _toggleMenuCommand = new RelayCommand(p =>
                    {
                        IsBurgerMenuOpen = !IsBurgerMenuOpen;
                    }, p => true);
                }

                return _toggleMenuCommand;
            }
        }

        #endregion

        #region ----------Methods----------

        private async void SetBook()
        {
            if (StoryInfo != null)
            {
                _book = EpubReader.Read(await _storyEndpoint.GetStoryForReading(StoryInfo.Id));

                // Gets all html files in epub file
                var chapters = _book.Resources.Html;
                var titles = _book.TableOfContents;
                
                foreach(var title in titles)
                {
                    ChapterTitles.Add(title.Title);
                }

                // Converting html to xaml for flow document
                var xamlForFlowDoc = HtmlToXamlConverter.ConvertHtmlToXaml(chapters.ElementAt(ChapterNumber).TextContent, true);

                SetDoc(xamlForFlowDoc);

                SpinnerVisibility = Visibility.Collapsed;
            }
        }

        private void ChangeViewModel(ObservableObject p)
        {
            // Set the main CurrentViewModel from ReadingPageModel to the MainPageModel
            p = new MainPageModel(_mapper, _apiHelper, _storyEndpoint, _eventAggregator);
            ((MainViewModel)App.Current.MainWindow.DataContext).CurrentViewModel = p;
        }

        private void ChangeChapter(string isMovingForward)
        {
            bool movingForward = Convert.ToBoolean(isMovingForward);

            // Logic for moving forward or backward in chapters
            if (_book.Resources.Html.Count - 1 != ChapterNumber && movingForward == true)
            {
                ChapterNumber++;
                SelectedChapterIndex = ChapterNumber;
            }
            else if(movingForward == false && ChapterNumber > 0)
            {
                ChapterNumber--;
                SelectedChapterIndex = ChapterNumber;
            }

            // Go to the top of the document when chapter changes
            StoryDocument.BringIntoView();
        }

        private void SetDoc(string xaml)
        {
            // Replace all '#em' values in the xaml
            // '#em' values won't allow the xaml reader to load
            xaml = Regex.Replace(xaml, @"([0-9]em)|([0-9]%)", "1");

            StringReader stringReader = new StringReader(xaml);

            StoryDocument = XamlReader.Load(XmlReader.Create(stringReader)) as FlowDocument;
        }

        //This changes chapters and sets the doc after the selected chapter index changes
        private void SelectChapter(object sender, EventArgs e)
        {
            // _book.Resource.Html.Count has all available pages of the eBook
            // ChapterTitles.Count has all the chapters that are available in the table of contents
            // The difference between them will indicate whether there is an extra page that the table of contents doesn't account for
            var diff = _book.Resources.Html.Count - ChapterTitles.Count;
            var xamlForFlowDoc = string.Empty;

            // If chapter was selected from chapter menu, the chapter number must be updated
            ChapterNumber = SelectedChapterIndex;

            // Some stories might have an extra page that isn't accounted for within the chapter list, so an offset must be used to accurately switch between chapters
            if(diff != 0)
                xamlForFlowDoc = HtmlToXamlConverter.ConvertHtmlToXaml(_book.Resources.Html.ElementAt(ChapterNumber + 1).TextContent, true);
            else
                xamlForFlowDoc = HtmlToXamlConverter.ConvertHtmlToXaml(_book.Resources.Html.ElementAt(ChapterNumber).TextContent, true);
            
            
            SetDoc(xamlForFlowDoc);

            IsBurgerMenuOpen = false;
        }
        #endregion
    }
}
