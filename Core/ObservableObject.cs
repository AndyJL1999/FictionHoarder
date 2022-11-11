using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace FictionHoarderWPF.Core
{
    public class ObservableObject : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName]string name = null)
        {
            //if ProperyChanged isn't null -> Invoke
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}
