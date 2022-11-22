using FictionHoarderWPF.Core;
using FictionHoarderWPF.Core.Interfaces;
using FictionUI_Library.API;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FictionHoarderWPF.MVVM.ViewModel
{
    public class AccountViewModel : ObservableObject, IViewModel
    {
        private readonly IApiHelper _apiHelper;

        public AccountViewModel(IApiHelper apiHelper)
        {
            _apiHelper = apiHelper;
        }

        public string Name => "Account";

        public string Username 
        { 
            get { return _apiHelper.LoggedInUser.Username; } 
            set
            {
                _apiHelper.LoggedInUser.Username = value;
            }
        }
        public string Email 
        { 
            get { return _apiHelper.LoggedInUser.Email; }
            set
            {
                _apiHelper.LoggedInUser.Email = value;
            } 
        }
    }
}
