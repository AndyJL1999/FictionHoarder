using FictionHoarder.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace FictionHoarder.MVVM.ViewModel
{
    class MainPageModel : ObservableObject
    {
        public ICommand HomeViewCommand { get; set; }
        public ICommand StoriesViewCommand { get; set; }
        public ICommand HistoryViewCommand { get; set; }
        public ICommand SearchViewCommand { get; set; }

        private NavigationStore _navigationStore;

        public ObservableObject CurrentView => _navigationStore.CurrentSubViewModel;

        public MainPageModel(NavigationStore navigationStore)
        {
            _navigationStore = navigationStore;

            _navigationStore.CurrentSubViewModelChanged += OnCurrentSubViewModelChanged;

            HomeViewCommand = new NavigateCommand<HomeViewModel>(navigationStore, () => new HomeViewModel(navigationStore), true);
            StoriesViewCommand = new NavigateCommand<StoriesViewModel>(navigationStore, () => new StoriesViewModel(navigationStore), true);
            HistoryViewCommand = new NavigateCommand<HistoryViewModel>(navigationStore, () => new HistoryViewModel(navigationStore), true);
            SearchViewCommand = new NavigateCommand<SearchViewModel>(navigationStore, () => new SearchViewModel(navigationStore), true);
        }

        private void OnCurrentSubViewModelChanged()
        {
            OnPropertyChanged(nameof(CurrentView));
        }
    }
}
