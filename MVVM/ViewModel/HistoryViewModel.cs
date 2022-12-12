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
        private ICommand _removeFromHistoryCommand;
        #endregion

        public event EventHandler? ChangeToReadView;

        public HistoryViewModel(IMapper mapper, IApiHelper apiHelper, IStoryEndpoint storyEndpoint,
            IEventAggregator eventAggregator)
        {
            _mapper = mapper;
            _apiHelper = apiHelper;
            _storyEndpoint = storyEndpoint;
            _eventAggregator = eventAggregator;

            ChangeToReadView += HistoryViewModel_ChangeToReadView;

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

        public ICommand RemoveFromHistoryCommand
        {
            get
            {
                if (_removeFromHistoryCommand is null)
                {
                    _removeFromHistoryCommand = new RelayCommand(p => RemoveStory((int)p), p => true);
                }

                return _removeFromHistoryCommand;
            }

        }

        #endregion

        #region ----------Methods----------
        private async void HistoryViewModel_ChangeToReadView(object sender, EventArgs e)
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

        private async void SetHistory()
        {
            List<StoryModel> payload = (List<StoryModel>)await _storyEndpoint.GetUserStoryHistory();

            var stories = _mapper.Map<IEnumerable<StoryDisplayModel>>(payload);

            StoriesRead = new ObservableCollection<StoryDisplayModel>(stories);

            //Send the first 4 stories to the home view model subscriber
            if(payload.Count < 4)
            {
                _eventAggregator.GetEvent<GetRecentHistoryEvent>().Publish(payload.GetRange(0, payload.Count));
            }
            else
            {
                _eventAggregator.GetEvent<GetRecentHistoryEvent>().Publish(payload.GetRange(0, 4));
            }
        }

        private async void RemoveStory(int storyId)
        {
            await _storyEndpoint.RemoveFromUserStoryHistory(storyId);

            var storyToRemove = StoriesRead.Where(s => s.Id == storyId).First();

            StoriesRead.Remove(storyToRemove);
        }
        #endregion
    }
}
