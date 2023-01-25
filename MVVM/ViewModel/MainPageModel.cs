using AutoMapper;
using FictionDataAccessLibrary.Data;
using FictionHoarderWPF.Core;
using FictionHoarderWPF.Core.Interfaces;
using FictionHoarderWPF.MVVM.Model;
using FictionUI_Library;
using FictionUI_Library.API;
using FictionUI_Library.EventAggregators;
using Prism.Events;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace FictionHoarderWPF.MVVM.ViewModel
{
    class MainPageModel : ObservableObject
    {
        #region ----------Fields----------
        private readonly IApiHelper _apiHelper;
        private readonly IEventAggregator _eventAggregator;
        private ICommand _changeViewCommand;
        private Visibility _searchVisibility;
        private IViewModel _currentSubViewModel;
        private List<IViewModel> _viewModels;
        private ObservableCollection<StoryDisplayModel> _stories;
        private ObservableCollection<StoryDisplayModel> _history;
        private string _userWelcome;
        private string _storyTitle;
        #endregion

        public event EventHandler? SearchBoxTextChanged;

        public MainPageModel(IMapper mapper, IApiHelper apiHelper, IStoryEndpoint storyEndpoint,
            IEventAggregator eventAggregator)
        {
            _apiHelper = apiHelper;
            _eventAggregator = eventAggregator;

            ViewModels.Add(new HomeViewModel(mapper, apiHelper, storyEndpoint, eventAggregator));
            ViewModels.Add(new SearchViewModel(mapper, storyEndpoint, eventAggregator));
            ViewModels.Add(new StoriesViewModel(mapper, apiHelper, storyEndpoint, eventAggregator));
            ViewModels.Add(new HistoryViewModel(mapper, apiHelper, storyEndpoint, eventAggregator));
            ViewModels.Add(new AccountViewModel(mapper, apiHelper, storyEndpoint, eventAggregator));
            
            CurrentSubViewModel = ViewModels.FirstOrDefault();

            SearchBoxTextChanged += MakeStoryQuery;

            SearchVisibility = Visibility.Collapsed;

            _userWelcome = _apiHelper.LoggedInUser.Username;

            _eventAggregator.GetEvent<UsernameCarrierEvent>().Subscribe((username) => { UserWelcome = username; });
        }

        #region ----------Properties----------
        public string UserWelcome 
        { 
            get => $"Welcome! {_userWelcome}"; 
            set
            {
                _userWelcome = value;
                OnPropertyChanged(nameof(UserWelcome));
            }
        }

        public string StoryTitle 
        { 
            get { return _storyTitle; }
            set
            {
                _storyTitle = value;
                OnPropertyChanged(nameof(StoryTitle));
                SearchBoxTextChanged?.Invoke(this, new EventArgs());
            }
        }

        public ICommand ChangeViewCommand
        {
            get
            {
                if(_changeViewCommand is null)
                {
                    _changeViewCommand = new RelayCommand(p => ChangeViewModel((IViewModel)p), p => p is IViewModel);
                }

                return _changeViewCommand;
            }
        }

        public Visibility SearchVisibility
        {
            get { return _searchVisibility; }
            set
            {
                _searchVisibility = value;
                OnPropertyChanged(nameof(SearchVisibility));
            }
        }

        public List<IViewModel> ViewModels
        {
            get
            {
                if (_viewModels is null)
                {
                    _viewModels = new List<IViewModel>();
                }

                return _viewModels;
            }
        }

        public IViewModel CurrentSubViewModel
        {
            get { return _currentSubViewModel; }
            set
            {
                if(_currentSubViewModel != value)
                {
                    _currentSubViewModel = value;
                    OnPropertyChanged(nameof(CurrentSubViewModel));
                }
            }
        }
        #endregion

        #region ----------Methods----------
        public void MakeStoryQuery(object? sender, EventArgs e)
        {

            if (CurrentSubViewModel.Name == "Stories")
            {
                var query = _stories.Where(s => s.Title.ToLower().Contains(StoryTitle.ToLower())).ToList();

                ((StoriesViewModel)CurrentSubViewModel).Stories = new ObservableCollection<StoryDisplayModel>(query);
            }
            else if(CurrentSubViewModel.Name == "History")
            {
                var query = _history.Where(s => s.Title.ToLower().Contains(StoryTitle.ToLower())).ToList();

                ((HistoryViewModel)CurrentSubViewModel).StoriesRead = new ObservableCollection<StoryDisplayModel>(query);
            }

        }

        private void ChangeViewModel(IViewModel viewModel)
        {
            if(!ViewModels.Contains(viewModel))
            {
                ViewModels.Add(viewModel);
            }

            if(viewModel.Name == "Stories")
            {
                //Remove a previous search and make search box visible
                StoryTitle = string.Empty;
                SearchVisibility = Visibility.Visible;

                //Set stories if it hasn't been set already
                if (_stories == null)
                {
                    _stories = ((StoriesViewModel)viewModel).Stories;
                }
            }
            else if(viewModel.Name == "History")
            {
                //Remove a previous search and make search box visible
                StoryTitle = string.Empty;
                SearchVisibility = Visibility.Visible;

                //Set history if it hasn't been set already
                if (_history == null)
                {
                    _history = ((HistoryViewModel)viewModel).StoriesRead;
                }
            }
            else //if the view model isn't History or Stories collapse the search box
            {
                SearchVisibility = Visibility.Collapsed;
            }

            //Set chosen view model
            CurrentSubViewModel = ViewModels.FirstOrDefault(vm => vm == viewModel);
        }
        #endregion
    }
}
