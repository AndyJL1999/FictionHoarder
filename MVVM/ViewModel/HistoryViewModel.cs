using AutoMapper;
using FictionHoarderWPF.Core;
using FictionHoarderWPF.Core.Interfaces;
using FictionHoarderWPF.MVVM.Model;
using FictionUI_Library;
using FictionUI_Library.API;
using FictionUI_Library.Models;
using Prism.Events;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace FictionHoarderWPF.MVVM.ViewModel
{
    class HistoryViewModel : ObservableObject, IViewModel
    {
        #region ----------Fields----------
        private readonly IMapper _mapper;
        private readonly IApiHelper _apiHelper;
        private readonly IStoryEndpoint _storyEndpoint;
        private readonly IEventAggregator _eventAggregator;
        private ObservableCollection<StoryDisplayModel> _storiesRead;
        private StoryDisplayModel _selectedStory;
        #endregion

        public event EventHandler? ChangeToReadView;

        public HistoryViewModel(IMapper mapper, IApiHelper apiHelper, IStoryEndpoint storyEndpoint,
            IEventAggregator eventAggregator)
        {
            _mapper = mapper;
            _apiHelper = apiHelper;
            _storyEndpoint = storyEndpoint;
            _eventAggregator = eventAggregator;

            ChangeToReadView += StoriesViewModel_ChangeToReadView;

            SetHistory();
        }

        #region ----------Properties----------
        public string Name => "History";

        public ObservableCollection<StoryDisplayModel> StoriesRead
        {
            get { return _storiesRead; }
            set 
            { 
                _storiesRead = value;
                OnPropertyChanged(nameof(StoriesRead));
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

        private async void SetHistory()
        {
            var payload = await _storyEndpoint.GetUserStoryHistory();

            var stories = _mapper.Map<IEnumerable<StoryDisplayModel>>(payload);

            StoriesRead = new ObservableCollection<StoryDisplayModel>(stories);
        }

        #endregion
    }
}
