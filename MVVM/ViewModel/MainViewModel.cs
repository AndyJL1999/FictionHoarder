using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FictionHoarderWPF.Core;

namespace FictionHoarderWPF.MVVM.ViewModel
{
    class MainViewModel : ObservableObject
    {
        private ObservableObject _currentViewModel;

        public ObservableObject CurrentViewModel 
        { 
            get { return _currentViewModel; }
            set
            {
                _currentViewModel = value;
                OnPropertyChanged(nameof(CurrentViewModel));
            }
        }

        public MainViewModel(ObservableObject viewModel)
        {
            CurrentViewModel = viewModel;
        }
    }
}
