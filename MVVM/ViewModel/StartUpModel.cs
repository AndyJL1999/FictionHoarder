using AutoMapper;
using FictionHoarderWPF.Core;
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
        private bool _onLoginForm = false;
        private ICommand _showHiddenFormCommand;
        private ICommand _enterMainPageCommand;
        private Visibility _signUpVisibility;
        private Visibility _loginVisibility;
        private readonly IMapper _mapper;

        new public event PropertyChangedEventHandler PropertyChanged;

        public StartUpModel(IMapper mapper)
        {
            _mapper = mapper;

            _signUpVisibility = Visibility.Visible;
            _loginVisibility = Visibility.Collapsed;
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
                    _enterMainPageCommand = new RelayCommand(p => ChangeViewModel((ObservableObject)p), p => p is ObservableObject);
                }

                return _enterMainPageCommand;
            }
        }

        private void ChangeViewModel(ObservableObject p)
        {
            p = new MainPageModel(_mapper);
            App.Current.MainWindow.DataContext = new MainViewModel(p);
        }

        private void ShowForm()
        {
            _onLoginForm = !_onLoginForm;

            if (_onLoginForm)
            {
                SignUpVisibility = Visibility.Collapsed;
                LoginVisibility = Visibility.Visible;
            }
            else
            {
                SignUpVisibility = Visibility.Visible;
                LoginVisibility = Visibility.Collapsed;
            }
        }
    }
}
