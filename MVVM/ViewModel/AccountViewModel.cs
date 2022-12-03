using AutoMapper;
using FictionHoarderWPF.Core;
using FictionHoarderWPF.Core.Interfaces;
using FictionUI_Library.API;
using Prism.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace FictionHoarderWPF.MVVM.ViewModel
{
    public class AccountViewModel : ObservableObject, IViewModel
    {
        private readonly IMapper _mapper;
        private readonly IApiHelper _apiHelper;
        private readonly IStoryEndpoint _storyEndpoint;
        private readonly IEventAggregator _eventAggregator;
        private ICommand _logOutCommand;

        public AccountViewModel(IMapper mapper, IApiHelper apiHelper, IStoryEndpoint storyEndpoint,
            IEventAggregator eventAggregator)
        {
            _mapper = mapper;
            _apiHelper = apiHelper;
            _storyEndpoint = storyEndpoint;
            _eventAggregator = eventAggregator;
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

        private void LogOut()
        {
            _apiHelper.ApiClient.DefaultRequestHeaders.Clear();
            _storyEndpoint.ClearCache();
            App.Current.MainWindow.DataContext = new MainViewModel(new StartUpModel(_mapper, _apiHelper, _storyEndpoint, _eventAggregator));
        }
    }
}
