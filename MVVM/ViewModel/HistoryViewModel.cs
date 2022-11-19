using FictionHoarderWPF.Core;
using FictionHoarderWPF.Core.Interfaces;
using FictionHoarderWPF.MVVM.Model;
using FictionUI_Library.Models;
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
        private ObservableCollection<StoryDisplayModel> _storiesRead;

        public HistoryViewModel()
        {

        }

        public string Name => "History";

        public ObservableCollection<StoryDisplayModel> StoriesRead
        {
            get { return _storiesRead; }
            set 
            { 
                _storiesRead = value;
                OnPropertyChanged(nameof(StoriesRead));
            }
        }


    }
}
