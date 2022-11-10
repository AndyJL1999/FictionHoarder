using FictionHoarder.Core;
using FictionHoarder.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace FictionHoarder.MVVM.ViewModel
{
    class HomeViewModel : ObservableObject, IViewModel
    {
        public string Name => "Home";
    }
}
