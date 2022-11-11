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
        private ICommand _changeViewCommand;
        private IViewModel _currentViewModel;
        private List<IViewModel> _viewModels;

        public MainPageModel()
        {
            ViewModels.Add(new HomeViewModel());
            ViewModels.Add(new SearchViewModel());
            ViewModels.Add(new StoriesViewModel());
            ViewModels.Add(new HistoryViewModel());
            

            CurrentViewModel = ViewModels.FirstOrDefault();
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

        public IViewModel CurrentViewModel
        {
            get { return _currentViewModel; }
            set
            {
                if(_currentViewModel != value)
                {
                    _currentViewModel = value;
                    OnPropertyChanged(nameof(CurrentViewModel));
                }
            }
        }

        private void ChangeViewModel(IViewModel viewModel)
        {
            if(!ViewModels.Contains(viewModel))
            {
                ViewModels.Add(viewModel);
            }

            CurrentViewModel = ViewModels.FirstOrDefault(vm => vm == viewModel);
        }
    }
}
