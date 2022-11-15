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

namespace FictionHoarderWPF.MVVM.ViewModel
{
    public class StoriesViewModel : ObservableObject, IViewModel
    {
        #region Fields
        private ObservableCollection<StoryDisplayModel> _stories;
        private StoryDisplayModel _selectedStory;
        private readonly IMapper _mapper;
        #endregion

        public event EventHandler? ChangeToReadView;

        //Constructor
        public StoriesViewModel(IMapper mapper)
        {
            _mapper = mapper;

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
        private void StoriesViewModel_ChangeToReadView(object sender, EventArgs e)
        {
            App.Current.MainWindow.DataContext = new MainViewModel(new ReadPageModel(_mapper, SelectedStory));
        }

        public async void SetStories()
        {
            //TODO - Replace SetStories Code
            var client = new HttpClient();
            using(HttpResponseMessage response = await client.GetAsync("https://localhost:7267/api/Story/2"))
            {
                var payload = await response.Content.ReadFromJsonAsync<IEnumerable<Story>>();
                var stories = _mapper.Map<IEnumerable<StoryDisplayModel>>(payload);


                Stories = new ObservableCollection<StoryDisplayModel>(stories);
            }

        }
        #endregion
    }
}
