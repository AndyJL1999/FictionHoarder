using FictionHoarderWPF.Core;
using FictionDataAccessLibrary.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using FictionHoarderWPF.Core.Interfaces;
using FictionHoarderWPF.MVVM.Model;
using System.Net.Http;
using System.Net.Http.Json;
using AutoMapper;
using Microsoft.Extensions.Configuration;
using System.Configuration;
using Microsoft.IdentityModel.Protocols;
using FictionUI_Library.API;
using FictionUI_Library.Models;
using Prism.Events;
using FictionUI_Library;

namespace FictionHoarderWPF.MVVM.ViewModel
{
    public class StoriesViewModel : ObservableObject, IViewModel
    {
        #region ----------Fields----------
        private ObservableCollection<StoryDisplayModel> _stories;
        private StoryDisplayModel _selectedStory;
        private readonly IMapper _mapper;
        private readonly IApiHelper _apiHelper;
        private readonly IStoryEndpoint _storyEndpoint;
        private readonly IEventAggregator _eventAggregator;
        #endregion

        public event EventHandler? ChangeToReadView;

        //Constructor
        public StoriesViewModel(IMapper mapper, IApiHelper apiHelper, IStoryEndpoint storyEndpoint,
            IEventAggregator eventAggregator)
        {
            _mapper = mapper;
            _apiHelper = apiHelper;
            _storyEndpoint = storyEndpoint;
            _eventAggregator = eventAggregator;

            ChangeToReadView += StoriesViewModel_ChangeToReadView;

            SetStories();

            //Event fires when a new story is added from the search view
            _eventAggregator.GetEvent<RefreshStoriesEvent>().Subscribe(() => 
            {
                ComingFromSearch = true;
                SetStories();
            });
        }

        #region ----------Properties----------
        private bool ComingFromSearch { get; set; } = false;

        public string Name => "Stories";

        public ObservableCollection<StoryDisplayModel> Stories
        {
            get { return _stories; }
            set
            {
                _stories = value;
                OnPropertyChanged(nameof(Stories));
            }
        }

        public StoryDisplayModel SelectedStory 
        {
            get { return _selectedStory; }
            set
            {
                _selectedStory = value;
                OnPropertyChanged(nameof(SelectedStory));
                ChangeToReadView?.Invoke(this, new EventArgs());
            } 
        }
        #endregion

        #region ----------Methods----------
        private async void StoriesViewModel_ChangeToReadView(object sender, EventArgs e)
        {
            var story = _mapper.Map<StoryModel>(SelectedStory);

            await _storyEndpoint.AddToStoryHistory(SelectedStory.Id);
            _storyEndpoint.StoryForCache = story;

            App.Current.MainWindow.DataContext = new MainViewModel(new ReadPageModel(_mapper, _apiHelper, _storyEndpoint, _eventAggregator));
            _eventAggregator.GetEvent<StorySelectionEvent>().Publish(story);
        }

        private async void SetStories()
        {
            var payload = await _storyEndpoint.GetUserStories(ComingFromSearch);
            ComingFromSearch = false;

            var stories = _mapper.Map<IEnumerable<StoryDisplayModel>>(payload);

            Stories = new ObservableCollection<StoryDisplayModel>(stories);
        }
        #endregion
    }
}
