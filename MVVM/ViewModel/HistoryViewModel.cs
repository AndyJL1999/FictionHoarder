﻿using AutoMapper;
using FictionHoarderWPF.Core;
using FictionHoarderWPF.Core.Interfaces;
using FictionHoarderWPF.MVVM.Model;
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
        private readonly IMapper _mapper;
        private readonly IApiHelper _apiHelper;
        private readonly IStoryEndpoint _storyEndpoint;
        private readonly IEventAggregator _eventAggregator;
        private ObservableCollection<StoryDisplayModel> _storiesRead;
        private StoryDisplayModel _selectedStory;

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

        private async void StoriesViewModel_ChangeToReadView(object sender, EventArgs e)
        {
            await _storyEndpoint.AddToStoryHistory(SelectedStory.Id);
            _storyEndpoint.StoryForCache = _mapper.Map<StoryModel>(SelectedStory);
            App.Current.MainWindow.DataContext = new MainViewModel(new ReadPageModel(_mapper, _apiHelper, _storyEndpoint, SelectedStory, _eventAggregator));
        }

        private async void SetHistory()
        {
            var payload = await _storyEndpoint.GetUserStoryHistory();

            var stories = _mapper.Map<IEnumerable<StoryDisplayModel>>(payload);

            StoriesRead = new ObservableCollection<StoryDisplayModel>(stories);
        }
    }
}
