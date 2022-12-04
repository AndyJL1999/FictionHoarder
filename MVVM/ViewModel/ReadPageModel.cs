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
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Markup;

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
        private ICommand _goToHomeCommand;
        private ICommand _changeChapterCommand;
        private int _chapterNumber = 1;

        #endregion

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
            get { return $"Chapter {ChapterNumber}"; }
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

        #endregion

        #region ----------Methods----------

        private void SetBook()
        {
            if (StoryInfo != null)
            {
                _book = EpubReader.Read(StoryInfo.EpubFile);

                //Gets all html files in epub file
                var chapters = _book.Resources.Html;

                //Converting html to xaml for flow document
                var xamlForFlowDoc = HtmlToXamlConverter.ConvertHtmlToXaml(chapters.ElementAt(ChapterNumber).TextContent, true);

                SetDoc(xamlForFlowDoc);
            }
        }

        private void ChangeViewModel(ObservableObject p)
        {
            p = new MainPageModel(_mapper, _apiHelper, _storyEndpoint, _eventAggregator);
            App.Current.MainWindow.DataContext = new MainViewModel(p);
        }

        private void ChangeChapter(string isMovingForward)
        {
            bool movingForward = Convert.ToBoolean(isMovingForward);

            //Logic for moving forward or backward in chapters
            if (_book.Resources.Html.Count - 1 != ChapterNumber && movingForward == true)
            {
                ChapterNumber++;
                var xamlForFlowDoc = HtmlToXamlConverter.ConvertHtmlToXaml(_book.Resources.Html.ElementAt(ChapterNumber).TextContent, true);

                SetDoc(xamlForFlowDoc);
            }
            else if(movingForward == false && ChapterNumber > 1)
            {
                ChapterNumber--;
                var xamlForFlowDoc = HtmlToXamlConverter.ConvertHtmlToXaml(_book.Resources.Html.ElementAt(ChapterNumber).TextContent, true);

                SetDoc(xamlForFlowDoc);
            }

            //Go to the top of the document when chapter changes
            StoryDocument.BringIntoView();
        }

        private void SetDoc(string xaml)
        {
            StringReader stringReader = new StringReader(xaml);
            System.Xml.XmlReader xmlReader = System.Xml.XmlReader.Create(stringReader);
            StoryDocument = XamlReader.Load(xmlReader) as FlowDocument;
        }

        #endregion
    }
}
