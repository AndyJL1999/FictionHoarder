using AutoMapper;
using FictionHoarderWPF.Core;
using FictionUI_Library.API;
using FictionUI_Library.EventAggregators;
using Prism.Events;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace FictionHoarderWPF.MVVM.ViewModel
{
    public class StartUpModel : ObservableObject
    {
        #region ----------Fields----------
        private bool _onLoginForm = true;
        private string _username;
        private string _email;
        private string _resultMessage;
        private ICommand _showHiddenFormCommand;
        private ICommand _enterMainPageCommand;
        private Visibility _signUpVisibility;
        private Visibility _loginVisibility;
        private Brush _resultColor;
        private readonly IMapper _mapper;
        private readonly IApiHelper _apiHelper;
        private readonly IStoryEndpoint _storyEndpoint;
        private readonly IEventAggregator _eventAggregator;

        #endregion

        public StartUpModel(IMapper mapper, IApiHelper apiHelper, IStoryEndpoint storyEndpoint,
            IEventAggregator eventAggregator)
        {
            _mapper = mapper;
            _apiHelper = apiHelper;
            _storyEndpoint = storyEndpoint;
            _eventAggregator = eventAggregator;

            _signUpVisibility = Visibility.Collapsed;
            _loginVisibility = Visibility.Visible;

            Username = string.Empty;
            Email = string.Empty;
            Password = string.Empty;
        }

        #region ----------Properties----------
        public Brush ResultColor
        {
            get { return _resultColor; }
            set
            {
                _resultColor = value;
                OnPropertyChanged(nameof(ResultColor));
            }
        }

        public string Username 
        {
            get { return _username; }
            set
            {
                _username = value;
                OnPropertyChanged(nameof(Username));
            }
        }

        public string Password { private get; set; }

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

                if(ResultMessage?.Length > 0)
                {
                    output = true;
                }
                return output; 
            }
        }

        public string ResultMessage 
        { 
            get { return _resultMessage; }
            set
            {
                _resultMessage = value;
                OnPropertyChanged(nameof(ResultMessage));
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
                    _showHiddenFormCommand = new RelayCommand(p => ShowForm(p as PasswordBox), p => true);
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
                if(_onLoginForm)
                {
                    ResultMessage = string.Empty;
                    var result = await _apiHelper.Authenticate(Email, Password);

                    await _apiHelper.GetUserInfo(result.Token);

                    p = new MainPageModel(_mapper, _apiHelper, _storyEndpoint, _eventAggregator);
                    App.Current.MainWindow.DataContext = new MainViewModel(p);

                    _eventAggregator.GetEvent<PasswordCarrierEvent>().Publish(Password);
                }
                else //on register form
                {
                    var result = await _apiHelper.Register(Username, Password.ToString(), Email);

                    ResultColor = new SolidColorBrush(Colors.LimeGreen);

                    ResultMessage = result.Trim('"');
                }
                
            }
            catch(NullReferenceException)
            {
                ResultColor = new SolidColorBrush(Colors.Red);
                ResultMessage = "Please don't leave any fields blank";
            }
            catch(Exception ex)
            {
                ResultColor = new SolidColorBrush(Colors.Red);

                if (ex.Message == "Unauthorized")
                    ResultMessage = "Wrong Email or Password";

                ResultMessage = ex.Message.Trim('"');
            }
            
        }

        private void ShowForm(PasswordBox passwordBox)
        {
            _onLoginForm = !_onLoginForm;

            if (_onLoginForm)
            {
                SignUpVisibility = Visibility.Collapsed;
                LoginVisibility = Visibility.Visible;
                ResultMessage = string.Empty;
                Username = string.Empty;
                Password = string.Empty;
                Email = string.Empty;
            }
            else
            {
                SignUpVisibility = Visibility.Visible;
                LoginVisibility = Visibility.Collapsed;
                ResultMessage = string.Empty;
                Username = string.Empty;
                Password = string.Empty;
                Email = string.Empty;
            }

            passwordBox.Password = Password;
        }
    }
}
