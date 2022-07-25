using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FictionHoarder.Core;

namespace FictionHoarder.MVVM.ViewModel
{
    class MainViewModel : ObservableObject
    {
        public RelayCommand HomeViewCommand { get; set; }
        public RelayCommand StoriesViewCommand { get; set; }
        public RelayCommand HistoryViewCommand { get; set; }

        public HomeViewModel HomeVM { get; set; }
        public StoriesViewModel StoriesVM { get; set; }
        public HistoryViewModel HistoryVM { get; set; }

        private object _currentView;

        public object CurrentView
        {
            get { return _currentView; }
            set 
            { 
                _currentView = value;
                OnPropertyChanged();
            }
        }

        public MainViewModel()
        {
            HomeVM = new HomeViewModel();
            StoriesVM = new StoriesViewModel();
            HistoryVM = new HistoryViewModel();

            CurrentView = HomeVM;

            HomeViewCommand = new RelayCommand(o =>
            {
                CurrentView = HomeVM;
            });

            StoriesViewCommand = new RelayCommand(o =>
            {
                CurrentView = StoriesVM;
            });

            HistoryViewCommand = new RelayCommand(o =>
            {
                CurrentView = HistoryVM;
            });
        }
    }
}
