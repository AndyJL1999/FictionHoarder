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
    /// Interaction logic for AccountView.xaml
    /// </summary>
    public partial class AccountView : UserControl
    {
        public AccountView()
        {
            InitializeComponent();

            DataContextChanged += AccountView_DataContextChanged;
        }

        private void AccountView_DataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (DataContext != null)
            {
                //Set password form
                CurrentPasswordBox.Password = ((dynamic)this.DataContext).Password;
            }
        }

        private void PasswordBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            if (((PasswordBox)sender).Name == "NewPasswordBox")
            {
                if (this.DataContext != null)
                {
                    ((dynamic)this.DataContext).NewPassword = ((PasswordBox)sender).Password;
                }
            }
            else if(((PasswordBox)sender).Name == "PasswordCheckBox")
            {
                if (this.DataContext != null)
                {
                    ((dynamic)this.DataContext).PasswordCheck = ((PasswordBox)sender).Password;
                }
            }
        }

        private void PasswordBox_IsEnabledChanged(object sender, DependencyPropertyChangedEventArgs e)
        {

            if (this.DataContext != null)
            {
                ((PasswordBox)sender).Password = string.Empty;
            }
        }
    }
}
