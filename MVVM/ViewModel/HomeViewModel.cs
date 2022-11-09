using FictionHoarder.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace FictionHoarder.MVVM.ViewModel
{
    class HomeViewModel : ObservableObject
    {
        public ICommand ToReadCommand { get; }

        public HomeViewModel(NavigationStore navigationStore)
        {
            ToReadCommand = new NavigateCommand<ReadPageModel>(navigationStore, () => new ReadPageModel(navigationStore), false);
        }
    }
}
