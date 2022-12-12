using FictionHoarderWPF.MVVM.Model;
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
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace FictionHoarderWPF.MVVM.View.UserControls
{
    /// <summary>
    /// Interaction logic for HomeItemControl.xaml
    /// </summary>
    public partial class HomeItemControl : UserControl
    {
        public HomeItemControl()
        {
            InitializeComponent();
        }

        public StoryDisplayModel ViewedStory
        {
            get { return (StoryDisplayModel)GetValue(StoryProperty); }
            set { SetValue(StoryProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty StoryProperty =
            DependencyProperty.Register("ViewedStory", typeof(StoryDisplayModel), typeof(HomeItemControl), new PropertyMetadata(null, SetValues));

        private static void SetValues(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            HomeItemControl homeCtrl = d as HomeItemControl;
            if (homeCtrl != null)
            {
                homeCtrl.DataContext = homeCtrl.ViewedStory;
            }
        }

        private void Border_MouseEnter(object sender, MouseEventArgs e)
        {
            Storyboard sb = this.FindResource("EnterHoverAnimation") as Storyboard;

            Storyboard.SetTarget(sb, sender as Border);

            sb.Begin();
        }

        private void Border_MouseLeave(object sender, MouseEventArgs e)
        {
            Storyboard sb = this.FindResource("ExitHoverAnimation") as Storyboard;

            Storyboard.SetTarget(sb, sender as Border);

            sb.Begin();
        }
    }
}
