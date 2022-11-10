using FictionHoarder.Core;
using FictionHoarder.MVVM.ViewModel;
using FictionDataAccessLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace FictionHoarder.MVVM.View
{
    /// <summary>
    /// Interaction logic for StoriesView.xaml
    /// </summary>
    public partial class StoriesView : UserControl
    {
        public StoriesView()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            //var viewModel = (StoriesViewModel)DataContext;
            //if (viewModel.ToReadCommand.CanExecute(null))
            //    viewModel.ToReadCommand.Execute(null);
            
        }
    }
}
