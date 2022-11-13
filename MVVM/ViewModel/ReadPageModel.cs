using FictionDataAccessLibrary.Data;
using FictionDataAccessLibrary.Models;
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
    class ReadPageModel : ObservableObject
    {
        private readonly Story _story;
        private ICommand _goToHomeCommand;

        public ReadPageModel(Story story)
        {
            _story = story;
        }

        public Story Story 
        { 
            get { return _story; } 
        }

        public ICommand GoToHomeCommand
        {
            get 
            {
                if (_goToHomeCommand is null)
                {
                    _goToHomeCommand = new RelayCommand(p => ChangeViewModel((ObservableObject)p), p => p is ObservableObject);
                }

                return _goToHomeCommand; 
            }
        }

        private void ChangeViewModel(ObservableObject p)
        {
            p = new MainPageModel();
            App.Current.MainWindow.DataContext = new MainViewModel(p);
        }
    }
}
