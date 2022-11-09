using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FictionHoarder.MVVM.ViewModel;

namespace FictionHoarder.Core
{
    public class NavigationStore
    {
        public event Action CurrentViewModelChanged;
        private ObservableObject _currentViewModel;

        public event Action CurrentSubViewModelChanged;
        private ObservableObject _currentSubViewModel;

        public ObservableObject CurrentViewModel
        {
            get => _currentViewModel;
            set
            {
                _currentViewModel = value;
                OnCurrentViewModelChanged();
            }
        }

        public ObservableObject CurrentSubViewModel
        {
            get => _currentSubViewModel;
            set
            {
                _currentSubViewModel = value;
                OnCurrentSubViewModelChanged();
            }
        }

        private void OnCurrentSubViewModelChanged()
        {
            CurrentSubViewModelChanged?.Invoke();
        }

        private void OnCurrentViewModelChanged()
        {
            CurrentViewModelChanged?.Invoke();
        }
    }
}
