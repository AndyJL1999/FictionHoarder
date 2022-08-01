using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FictionHoarder.Core
{
    public class NavigateCommand<TViewModel> : RelayCommand
        where TViewModel : ObservableObject
    {
        private readonly NavigationStore _navigationStore;
        private readonly Func<TViewModel> _createViewModel;
        private readonly Func<TViewModel> _createSubViewModel;

        public NavigateCommand(NavigationStore navigationStore, Func<TViewModel> createViewModel, bool isSub)
        {
            _navigationStore = navigationStore;
            if(isSub)
            {
                _createSubViewModel = createViewModel;
            }else
                _createViewModel = createViewModel;
        }

        public override void Execute(object parameter)
        {
            if (_createViewModel is null)
            {
                _navigationStore.CurrentSubViewModel = _createSubViewModel();
                Console.WriteLine("hi");
            }
            else
            {
                _navigationStore.CurrentViewModel = _createViewModel();
                Console.WriteLine("Yo~");
            }
        }
    }
}
