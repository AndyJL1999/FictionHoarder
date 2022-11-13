using FictionDataAccessLibrary.Models;
using FictionHoarderWPF.Core;
using FictionHoarderWPF.Core.Interfaces;
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
        private ObservableCollection<Story> _storiesRead;

        public string Name => "History";

        new public event PropertyChangedEventHandler PropertyChanged;

        public ObservableCollection<Story> StoriesRead
        {
            get { return _storiesRead; }
            set 
            { 
                _storiesRead = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(StoriesRead)));
            }
        }


    }
}
