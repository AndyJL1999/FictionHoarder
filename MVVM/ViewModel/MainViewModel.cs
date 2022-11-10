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
        public ObservableObject CurrentViewModel { get; set; }

        public MainViewModel()
        {
            CurrentViewModel = new MainPageModel();
        }
    }
}
