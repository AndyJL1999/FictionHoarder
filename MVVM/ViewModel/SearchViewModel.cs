using EpubSharp;
using FictionHoarderWPF.Core;
using FictionHoarderWPF.Core.Interfaces;
using FictionHoarderWPF.MVVM.Model;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Text.RegularExpressions;
using System.Web;
using AutoMapper;
using FictionUI_Library.API;
using FictionUI_Library.Models;
using Prism.Events;
using FictionUI_Library.EventAggregators;
using System.Windows.Media;

namespace FictionHoarderWPF.MVVM.ViewModel
{
    class SearchViewModel : ObservableObject, IViewModel
    {
        #region ----------Fields----------
        private readonly IMapper _mapper;
        private readonly IStoryEndpoint _storyEndpoint;
        private readonly IEventAggregator _eventAggregator;
        private ICommand _browseFilesCommand;
        private ICommand _saveStoryCommand;
        private ICommand _clearInfoCommand;
        private Brush _resultColor;
        private EpubBook _book;
        private StoryDisplayModel _story;
        private Visibility _storyConfirmationVisibility;
        private string _resultText;
        #endregion

        public SearchViewModel(IMapper mapper, IStoryEndpoint storyEndpoint, IEventAggregator eventAggregator)
        {
            _mapper = mapper;
            _storyEndpoint = storyEndpoint;
            _eventAggregator = eventAggregator;

            _story = new StoryDisplayModel();
            _storyConfirmationVisibility = Visibility.Collapsed;
        }

        #region ----------Properties-----------
        public string Name => "Search";

        public StoryDisplayModel Story
        {
            get { return _story; }
            set 
            { 
                _story = value; 
                OnPropertyChanged(nameof(Story));
            }
        }

        public Brush ResultColor 
        {
            get { return _resultColor; }
            set
            {
                _resultColor = value;
                OnPropertyChanged(nameof(ResultColor));
            }
        }

        public string ResultText 
        { 
            get { return _resultText; }
            set
            {
                _resultText = value;
                OnPropertyChanged(nameof(ResultText));
            }
        }

        public ICommand BrowseFilesCommand
        {
            get
            {
                if (_browseFilesCommand is null)
                {
                    _browseFilesCommand = new RelayCommand(p => SearchFiles(), p => true);
                }

                return _browseFilesCommand;
            }
        }

        public ICommand SaveStoryCommand
        {
            get
            {
                if (_saveStoryCommand is null)
                {
                    _saveStoryCommand = new RelayCommand(p => AddToLibrary(), p => true);
                }

                return _saveStoryCommand;
            }
        }

        public ICommand ClearInfoCommand
        {
            get
            {
                if (_clearInfoCommand is null)
                {
                    _clearInfoCommand = new RelayCommand(p => ClearStoryInfo(), p => true);
                }

                return _clearInfoCommand;
            }
        }

        public Visibility StoryConfirmationVisibility
        {
            get { return _storyConfirmationVisibility; }
            set
            {
                _storyConfirmationVisibility = value;
                OnPropertyChanged(nameof(StoryConfirmationVisibility));
            }
        }

        #endregion

        #region ----------Methods----------
        private void ClearStoryInfo()
        {
            Story.Title = string.Empty;
            Story.Author = string.Empty;
            Story.Chapters = string.Empty;
            Story.Summary = string.Empty;
            Story.EpubFile = string.Empty;

            StoryConfirmationVisibility = Visibility.Collapsed;
        }

        private async void AddToLibrary()
        {
            var story = _mapper.Map<StoryModel>(Story);
            var stories = await _storyEndpoint.GetUserStories(false);

            var comparer = stories.Where(s => s.Title == Story.Title && s.Author == Story.Author
                && s.Chapters == Story.Chapters && s.EpubFile == Story.EpubFile
                && s.Summary == Story.Summary).FirstOrDefault();

            ResultColor = new SolidColorBrush(Colors.Red);
            ResultText = "Story may already exist in your library";

            if (story != null && comparer is null)
            {
                await _storyEndpoint.InsertNewStory(story);
                _storyEndpoint.StoryForCache = story;
                _eventAggregator.GetEvent<UpdateEvent>().Publish();

                ResultColor = new SolidColorBrush(Colors.LimeGreen);
                ResultText = "Upload Successful!";
            }

            ClearStoryInfo();
        }

        private async void SearchFiles()
        {
            //Opens file explorer
            OpenFileDialog dialog = new OpenFileDialog();

            //Setting filter for the files that will be shown
            dialog.Filter = "EPUB files (*.epub)|*.epub";
            dialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);

            //If file explorer opens successfully set the chosen file as the EpubFile for story
            if (dialog.ShowDialog() == true)
            {
                string fileName = dialog.FileName;

                _book = EpubReader.Read(fileName);
                Story.Title = _book.Title;
                Story.Author = _book.Authors.First();
                Story.Chapters = (_book.Resources.Html.Count - 1).ToString();
                Story.EpubFile = fileName;

                //Decode Html
                string htmlToDecode = HttpUtility.HtmlDecode(_book.Resources.Html.ElementAt(0).TextContent);
                //Get summary from html
                var htmlGroups = Regex.Matches(htmlToDecode, @"</span>\s*(.+?)\s*<br />");
                //Add summary to story model
                if (htmlGroups.Count > 0)
                    Story.Summary = htmlGroups.ElementAt(1).Groups[1].Value;
                else
                    Story.Summary = "No Summary";

                StoryConfirmationVisibility = Visibility.Visible;
            }
        }

#endregion
    }
}
