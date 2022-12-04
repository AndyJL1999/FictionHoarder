using AutoMapper;
using FictionHoarderWPF.Core;
using FictionHoarderWPF.Core.Interfaces;
using FictionUI_Library.API;
using FictionUI_Library.EventAggregators;
using Prism.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;

namespace FictionHoarderWPF.MVVM.ViewModel
{
    public class AccountViewModel : ObservableObject, IViewModel
    {
        #region ----------Fields----------
        private readonly IMapper _mapper;
        private readonly IApiHelper _apiHelper;
        private readonly IStoryEndpoint _storyEndpoint;
        private readonly IEventAggregator _eventAggregator;
        private ICommand _logOutCommand;
        private ICommand _showEditCommand;
        private ICommand _updateCommand;
        private Visibility _editVisibility;
        private Brush _updateColor;
        private bool _enableEdit;
        private bool _enablePasswordBox;
        private string _updateText;
        private string _username;
        private string _email;
        private string _newUsername;
        private string _newEmail;
        private string _newPassword;
        #endregion

        public AccountViewModel(IMapper mapper, IApiHelper apiHelper, IStoryEndpoint storyEndpoint,
            IEventAggregator eventAggregator)
        {
            _mapper = mapper;
            _apiHelper = apiHelper;
            _storyEndpoint = storyEndpoint;
            _eventAggregator = eventAggregator;

            _eventAggregator.GetEvent<PasswordCarrierEvent>().Subscribe((password) => { Password = password; });

            _editVisibility = Visibility.Collapsed;
            _enableEdit = true;

            _username = apiHelper.LoggedInUser.Username;
            _email = apiHelper.LoggedInUser.Email;
            _newUsername = Username;
            _newEmail = Email;
        }

        #region ----------Properties----------
        public string Name => "Account";

        public string UpdateText 
        { 
            get { return _updateText; }
            set
            {
                _updateText = value;
                OnPropertyChanged(nameof(UpdateText));
            }
        }

        public Brush UpdateColor
        { 
            get { return _updateColor; }
            set
            {
                _updateColor = value;
                OnPropertyChanged(nameof(UpdateColor));
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

        public string Email 
        { 
            get { return _email; }
            set
            {
                _email = value;
                OnPropertyChanged(nameof(Email));
            } 
        }

        public string Password  { get; set; }

        public string PasswordCheck { private get; set; }

        public string NewUsername 
        { 
            get { return _newUsername; } 
            set
            {
                _newUsername = value;
                OnPropertyChanged(nameof(NewUsername));
            }
        }

        public string NewEmail 
        { 
            get { return _newEmail; }
            set
            {
                _newEmail = value;
                OnPropertyChanged(nameof(NewEmail));
            }
        }

        public string NewPassword
        {
            get { return _newPassword; }
            set
            {
                _newPassword = value;
                OnPropertyChanged(nameof(NewPassword));
            }
        }

        public bool EnableEdit 
        { 
            get { return _enableEdit; }
            set
            {
                _enableEdit = value;
                OnPropertyChanged(nameof(EnableEdit));
            } 
        }

        public bool EnablePasswordBox
        {
            get { return _enablePasswordBox; }
            set
            {
                _enablePasswordBox = value;
                OnPropertyChanged(nameof(EnablePasswordBox));
            }
        }

        public Visibility EditVisibility
        {
            get { return _editVisibility; }
            set
            {
                _editVisibility = value;
                OnPropertyChanged(nameof(EditVisibility));
            } 
        }

        public ICommand UpdateCommand
        {
            get
            {
                if (_updateCommand is null)
                {
                    _updateCommand = new RelayCommand(p => UpdateAccount(), p => true);
                }

                return _updateCommand;
            }
        }

        public ICommand ShowEditCommand
        {
            get
            {
                if (_showEditCommand is null)
                {
                    _showEditCommand = new RelayCommand(p => ShowEdit(), p => true);
                }

                return _showEditCommand;
            }
        }

        public ICommand LogOutCommand 
        {
            get
            {
                if (_logOutCommand is null)
                {
                    _logOutCommand = new RelayCommand(p => LogOut(), p => true);
                }

                return _logOutCommand;
            }
        }
        #endregion

        #region ----------Methods----------
        private void ShowEdit()
        {
            EnableEdit = !EnableEdit;

            //Once edit button is disabled show edit form
            if (EnableEdit == false)
            {
                EditVisibility = Visibility.Visible;
                EnablePasswordBox = true;
            }
            else
            {
                //If edit is canceled re-enable edit button and hide edit form
                EditVisibility = Visibility.Collapsed;
                PasswordCheck = string.Empty;
                NewPassword = string.Empty;
                EnablePasswordBox = false;
            }
        }

        private async void UpdateAccount()
        {
            if(PasswordCheck == Password)//if Current Password == User Password
            {
                var response = await _apiHelper.UpdateUser(NewUsername, NewEmail, NewPassword);

                UpdateColor = new SolidColorBrush(Colors.LimeGreen);
                UpdateText = response;

                Username = NewUsername;
                Email = NewEmail;

                EditVisibility = Visibility.Collapsed;
                PasswordCheck = string.Empty;
                NewPassword = string.Empty;
                EnableEdit = true;
                EnablePasswordBox = false;

                _eventAggregator.GetEvent<UsernameCarrierEvent>().Publish(Username);
            }
            else
            {
                UpdateColor = new SolidColorBrush(Colors.Red);
                UpdateText = "Incorrect password";
                PasswordCheck = string.Empty;
                NewPassword = string.Empty;
            }

        }

        private void LogOut()
        {
            _apiHelper.ApiClient.DefaultRequestHeaders.Clear();
            _storyEndpoint.ClearCache();
            App.Current.MainWindow.DataContext = new MainViewModel(new StartUpModel(_mapper, _apiHelper, _storyEndpoint, _eventAggregator));
        }
        #endregion
    }
}
