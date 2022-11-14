using AutoMapper;
using FictionDataAccessLibrary.Data;
using FictionHoarderWPF.Core;
using FictionHoarderWPF.Core.Interfaces;
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
        private ICommand _changeViewCommand;
        private IViewModel _currentSubViewModel;
        private List<IViewModel> _viewModels;

        public MainPageModel(IMapper mapper)
        {
            _mapper = mapper;

            ViewModels.Add(new HomeViewModel());
            ViewModels.Add(new SearchViewModel());
            ViewModels.Add(new StoriesViewModel(_mapper));
            ViewModels.Add(new HistoryViewModel());
            

            CurrentSubViewModel = ViewModels.FirstOrDefault();
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
