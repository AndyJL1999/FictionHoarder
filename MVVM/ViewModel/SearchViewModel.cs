using FictionHoarder.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace FictionHoarder.MVVM.ViewModel
{
    class SearchViewModel : ObservableObject
    {
        public ICommand ToReadCommand { get; }

        public SearchViewModel(NavigationStore navigationStore)
        {
            ToReadCommand = new NavigateCommand<ReadPageModel>(navigationStore, () => new ReadPageModel(navigationStore), false);
        }
    }
}
