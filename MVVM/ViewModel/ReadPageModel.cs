using FictionHoarder.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace FictionHoarder.MVVM.ViewModel
{
    class ReadPageModel : ObservableObject
    {
        public ICommand ToHomeCommand { get; }

        public ReadPageModel(NavigationStore navigationStore)
        {
            ToHomeCommand = new NavigateCommand<MainPageModel>(navigationStore, () => new MainPageModel(navigationStore), false);
        }
    }
}
