using AutoMapper;
using FictionDataAccessLibrary.Data;
using FictionDataAccessLibrary.Models;
using FictionHoarderWPF.Core;
using FictionHoarderWPF.Core.Interfaces;
using FictionHoarderWPF.MVVM.Model;
using FictionUI_Library.API;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace FictionHoarderWPF.MVVM.ViewModel
{
    class ReadPageModel : ObservableObject
    {
        private readonly IMapper _mapper;
        private readonly IApiHelper _apiHelper;
        private readonly StoryDisplayModel _story;
        private ICommand _goToHomeCommand;

        public ReadPageModel(IMapper mapper, IApiHelper apiHelper,StoryDisplayModel story)
        {
            _mapper = mapper;
            _apiHelper = apiHelper;
            _story = story;
        }

        public StoryDisplayModel Story 
        { 
            get { return _story; } 
        }

        public ICommand GoToHomeCommand
        {
            get 
            {
                if (_goToHomeCommand is null)
                {
                    _goToHomeCommand = new RelayCommand(p => ChangeViewModel((ObservableObject)p), p => p is ObservableObject);
                }

                return _goToHomeCommand; 
            }
        }

        private void ChangeViewModel(ObservableObject p)
        {
            p = new MainPageModel(_mapper, _apiHelper);
            App.Current.MainWindow.DataContext = new MainViewModel(p);
        }
    }
}
