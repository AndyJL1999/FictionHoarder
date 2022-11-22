using AutoMapper;
using FictionHoarderWPF.Core;
using FictionHoarderWPF.Core.Interfaces;
using FictionHoarderWPF.MVVM.Model;
using FictionUI_Library.API;
using FictionUI_Library.Models;
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
        private ObservableCollection<StoryDisplayModel> _storiesRead;
        private StoryDisplayModel _selectedStory;

        public event EventHandler? ChangeToReadView;

        public HistoryViewModel(IMapper mapper, IApiHelper apiHelper)
        {
            _mapper = mapper;
            _apiHelper = apiHelper;

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
            await _apiHelper.AddToStoryHistory(SelectedStory.Id);
            App.Current.MainWindow.DataContext = new MainViewModel(new ReadPageModel(_mapper, _apiHelper, SelectedStory));
        }

        private async void SetHistory()
        {
            var payload = await _apiHelper.GetUserStoryHistory();

            var stories = _mapper.Map<IEnumerable<StoryDisplayModel>>(payload);

            StoriesRead = new ObservableCollection<StoryDisplayModel>(stories);
        }
    }
}
