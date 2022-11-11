using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FictionHoarderWPF.Core;

namespace FictionHoarderWPF.MVVM.ViewModel
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
