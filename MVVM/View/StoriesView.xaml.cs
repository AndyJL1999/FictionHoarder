﻿using FictionHoarderWPF.Core;
using FictionHoarderWPF.MVVM.ViewModel;
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

namespace FictionHoarderWPF.MVVM.View
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

        private void ListItemControl_PreviewRightMouseDown(object sender, MouseButtonEventArgs e)
        {
            e.Handled = true;
        }
    }
}
