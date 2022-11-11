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

namespace FictionHoarderWPF.MVVM.View.UserControls
{
    /// <summary>
    /// Interaction logic for ListItemControl.xaml
    /// </summary>
    public partial class ListItemControl : UserControl
    {
        public Story Story
        {
            get { return (Story)GetValue(StoryProperty); }
            set { SetValue(StoryProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty StoryProperty =
            DependencyProperty.Register("Story", typeof(Story), typeof(ListItemControl), new PropertyMetadata(null, SetValues));

        private static void SetValues(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ListItemControl noteCtrl = d as ListItemControl;
            if (noteCtrl != null)
                noteCtrl.DataContext = noteCtrl.Story;
        }


        public ListItemControl()
        {
            InitializeComponent();
        }
    }
}
