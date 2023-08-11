using AutoMapper;
using FictionHoarderWPF.Core;
using FictionHoarderWPF.Core.Interfaces;
using FictionHoarderWPF.MVVM.Model;
using FictionUI_Library.API;
using FictionUI_Library.EventAggregators;
using FictionUI_Library.Models;
using Prism.Events;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Animation;

namespace FictionHoarderWPF.MVVM.ViewModel
{
    class HomeViewModel : ObservableObject, IViewModel
    {
        private readonly IMapper _mapper;
        private readonly IApiHelper _apiHelper;
        private readonly IStoryEndpoint _storyEndpoint;
        private readonly IEventAggregator _eventAggregator;
        private Visibility _emptyStoriesTextVisibility;
        private ObservableCollection<StoryDisplayModel> _storiesViewed;
        private StoryDisplayModel _selectedStory;

        public event EventHandler ChangeToReadView;

        public HomeViewModel(IMapper mapper, IApiHelper apiHelper, IStoryEndpoint storyEndpoint, IEventAggregator eventAggregator)
        {
            _mapper = mapper;
            _apiHelper = apiHelper;
            _storyEndpoint = storyEndpoint;
            _eventAggregator = eventAggregator;

            ChangeToReadView += HomeViewModel_ChangeToReadView;

            //Populate Stories viewed with the first four stories from the history list
            _eventAggregator.GetEvent<GetRecentHistoryEvent>().Subscribe((stories) =>
            {
                StoriesViewed = _mapper.Map<ObservableCollection<StoryDisplayModel>>(stories);

                //only show this text if there are no stories to show
                EmptyStoriesTextVisibility = (StoriesViewed.Count == 0) ? Visibility.Visible : Visibility.Collapsed;
            });

        }

        public string Name => "Home";

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

        public ObservableCollection<StoryDisplayModel> StoriesViewed 
        { 
            get { return _storiesViewed; } 
            set
            {
                _storiesViewed = value;
                OnPropertyChanged(nameof(StoriesViewed));
            }
        }

        public Visibility EmptyStoriesTextVisibility
        { 
            get { return _emptyStoriesTextVisibility; }
            set
            {
                _emptyStoriesTextVisibility = value;
                OnPropertyChanged(nameof(EmptyStoriesTextVisibility));
            }
        }

        private async void HomeViewModel_ChangeToReadView(object sender, EventArgs e)
        {
            if (SelectedStory != null)
            {
                var story = _mapper.Map<StoryModel>(SelectedStory);

                await _storyEndpoint.AddToStoryHistory(SelectedStory.Id);
                _storyEndpoint.StoryForCache = story;

                App.Current.MainWindow.DataContext = new MainViewModel(new ReadPageModel(_mapper, _apiHelper, _storyEndpoint, _eventAggregator));

                //Send selected story info to reading view model subscriber
                _eventAggregator.GetEvent<StorySelectionEvent>().Publish(story);
            }
        }
    }
}
