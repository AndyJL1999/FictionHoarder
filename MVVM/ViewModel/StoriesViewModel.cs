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

namespace FictionHoarderWPF.MVVM.ViewModel
{
    public class StoriesViewModel : ObservableObject, IViewModel
    {
        #region Fields
        private ObservableCollection<StoryDisplayModel> _stories;
        private StoryDisplayModel _selectedStory;
        private readonly IMapper _mapper;
        private readonly IApiHelper _apiHelper;
        private readonly IStoryEndpoint _storyEndpoint;
        #endregion

        public event EventHandler? ChangeToReadView;

        //Constructor
        public StoriesViewModel(IMapper mapper, IApiHelper apiHelper, IStoryEndpoint storyEndpoint)
        {
            _mapper = mapper;
            _apiHelper = apiHelper;
            _storyEndpoint = storyEndpoint;

            ChangeToReadView += StoriesViewModel_ChangeToReadView;

            SetStories();
        }


        #region Properties
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

        #region Methods
        private async void StoriesViewModel_ChangeToReadView(object sender, EventArgs e)
        {
            await _storyEndpoint.AddToStoryHistory(SelectedStory.Id);
            _storyEndpoint.StoryForCache = _mapper.Map<StoryModel>(SelectedStory);
            App.Current.MainWindow.DataContext = new MainViewModel(new ReadPageModel(_mapper, _apiHelper, _storyEndpoint, SelectedStory));
        }

        private async void SetStories()
        {
            var payload = await _storyEndpoint.GetUserStories();

            var stories = _mapper.Map<IEnumerable<StoryDisplayModel>>(payload);

            Stories = new ObservableCollection<StoryDisplayModel>(stories);
        }
        #endregion
    }
}
