using AutoMapper;
using EpubSharp;
using FictionHoarderWPF.Core;
using FictionHoarderWPF.Core.Interfaces;
using FictionHoarderWPF.MVVM.Model;
using FictionUI_Library.API;
using HTMLConverter;
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
        private readonly StoryDisplayModel _storyInfo;
        private EpubBook _book;
        private FlowDocument _storyDocument;
        private ICommand _goToHomeCommand;
        private ICommand _changeChapterCommand;
        private int _chapterNumber = 1;

        #endregion

        public ReadPageModel(IMapper mapper, IApiHelper apiHelper, IStoryEndpoint storyEndpoint, StoryDisplayModel storyInfo)
        {
            _mapper = mapper;
            _apiHelper = apiHelper;
            _storyEndpoint = storyEndpoint;
            _storyInfo = storyInfo;
            SetBook();
        }

        #region -----------Properties----------

        public StoryDisplayModel StoryInfo
        {
            get { return _storyInfo; }
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
            _book = EpubReader.Read(@"C:\\Users\\andyl\\OneDrive\\Uploads\\Documents\\ePub files\\HP\\HP - Other\A Flower for the Soul - TheBlack'sResurgence.epub");

            var chapters = _book.Resources.Html;
            var xamlForFlowDoc = HtmlToXamlConverter.ConvertHtmlToXaml(chapters.ElementAt(ChapterNumber).TextContent, true);

            SetDoc(xamlForFlowDoc);
        }

        private void ChangeViewModel(ObservableObject p)
        {
            p = new MainPageModel(_mapper, _apiHelper, _storyEndpoint);
            App.Current.MainWindow.DataContext = new MainViewModel(p);
        }

        private void ChangeChapter(string isMovingForward)
        {
            bool movingForward = Convert.ToBoolean(isMovingForward);

            if (_book.Resources.Html.Count != ChapterNumber && movingForward == true)
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
