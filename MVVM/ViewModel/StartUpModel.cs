using AutoMapper;
using FictionHoarderWPF.Core;
using FictionUI_Library.API;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace FictionHoarderWPF.MVVM.ViewModel
{
    public class StartUpModel : ObservableObject
    {
        #region Fields
        private bool _onLoginForm = false;
        private string _username;
        private string _password;
        private string _email;
        private string _errorMessage;
        private ICommand _showHiddenFormCommand;
        private ICommand _enterMainPageCommand;
        private Visibility _signUpVisibility;
        private Visibility _loginVisibility;
        private readonly IMapper _mapper;
        private readonly IApiHelper _apiHelper;

        #endregion

        public StartUpModel(IMapper mapper, IApiHelper apiHelper)
        {
            _mapper = mapper;
            _apiHelper = apiHelper;

            _signUpVisibility = Visibility.Visible;
            _loginVisibility = Visibility.Collapsed;
        }

        #region Properties
        public string Username 
        {
            get { return _username; }
            set
            {
                _username = value;
                OnPropertyChanged(nameof(Username));
            }
        }

        public string Password
        {
            get { return _password; }
            set
            {
                _password = value;
                OnPropertyChanged(nameof(Password));
            }
        }

        public string Email
        {
            get { return _email; }
            set
            {
                _email = value;
                OnPropertyChanged(nameof(Email));
            }
        }

        public bool IsErrorVisible 
        {
            get 
            {
                bool output = false;

                if(ErrorMessage?.Length > 0)
                {
                    output = true;
                }
                return output; 
            }
        }

        public string ErrorMessage 
        { 
            get { return _errorMessage; }
            set
            {
                _errorMessage = value;
                OnPropertyChanged(nameof(ErrorMessage));
                OnPropertyChanged(nameof(IsErrorVisible));
            }
        }

        public Visibility LoginVisibility 
        { 
            get { return _loginVisibility; }
            set
            {
                _loginVisibility = value;
                OnPropertyChanged(nameof(LoginVisibility));
            } 
        } 

        public Visibility SignUpVisibility 
        { 
            get { return _signUpVisibility; }
            set
            {
                _signUpVisibility = value;
                OnPropertyChanged(nameof(SignUpVisibility));
            } 
        }

        public ICommand ShowHiddenFormCommand
        {
            get
            {
                if (_showHiddenFormCommand is null)
                {
                    _showHiddenFormCommand = new RelayCommand(p => ShowForm(), p => true);
                }

                return _showHiddenFormCommand;
            }
        }

        public ICommand EnterMainPageCommand
        {
            get
            {
                if (_enterMainPageCommand is null)
                {
                    _enterMainPageCommand = new RelayCommand(p => ChangeViewModelAsync((ObservableObject)p), p => p is ObservableObject);
                }

                return _enterMainPageCommand;
            }
        }

        #endregion

        private async Task ChangeViewModelAsync(ObservableObject p)
        {
            try
            {
                var result = await _apiHelper.Authenticate(Email, Password);

                await _apiHelper.GetUserInfo(result.Token);

                p = new MainPageModel(_mapper, _apiHelper);
                App.Current.MainWindow.DataContext = new MainViewModel(p);
            }
            catch(Exception ex)
            {
                ErrorMessage = ex.Message;
            }
            
        }

        private void ShowForm()
        {
            _onLoginForm = !_onLoginForm;

            if (_onLoginForm)
            {
                SignUpVisibility = Visibility.Collapsed;
                LoginVisibility = Visibility.Visible;
                ErrorMessage = string.Empty;
                Username = string.Empty;
                Password = string.Empty;
                Email = string.Empty;
            }
            else
            {
                SignUpVisibility = Visibility.Visible;
                LoginVisibility = Visibility.Collapsed;
                ErrorMessage = string.Empty;
                Username = string.Empty;
                Password = string.Empty;
                Email = string.Empty;
            }
        }
    }
}
