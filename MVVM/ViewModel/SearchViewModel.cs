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
    class SearchViewModel : ObservableObject, IViewModel
    {
        public string Name => "Search";

    }
}
