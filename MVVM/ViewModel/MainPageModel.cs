using AutoMapper;
using FictionDataAccessLibrary.Data;
using FictionHoarderWPF.Core;
using FictionHoarderWPF.Core.Interfaces;
using FictionUI_Library.API;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace FictionHoarderWPF.MVVM.ViewModel
{
    class MainPageModel : ObservableObject
    {
        private readonly IMapper _mapper;
        private readonly IApiHelper _apiHelper;
        private ICommand _changeViewCommand;
        private IViewModel _currentSubViewModel;
        private List<IViewModel> _viewModels;

        public MainPageModel(IMapper mapper, IApiHelper apiHelper)
        {
            _mapper = mapper;
            _apiHelper = apiHelper;

            ViewModels.Add(new HomeViewModel());
            ViewModels.Add(new SearchViewModel());
            ViewModels.Add(new StoriesViewModel(_mapper, _apiHelper));
            ViewModels.Add(new HistoryViewModel());
            

            CurrentSubViewModel = ViewModels.FirstOrDefault();
        }

        public string UserWelcome { get => $"Welcome! {_apiHelper.User}"; }

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

        private void ChangeViewModel(IViewModel viewModel)
        {
            if(!ViewModels.Contains(viewModel))
            {
                ViewModels.Add(viewModel);
            }

            CurrentSubViewModel = ViewModels.FirstOrDefault(vm => vm == viewModel);
        }
    }
}
