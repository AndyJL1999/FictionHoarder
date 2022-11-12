using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FictionDataAccessLibrary.Data;
using FictionHoarderWPF.Core;

namespace FictionHoarderWPF.MVVM.ViewModel
{
    class MainViewModel : ObservableObject
    {
        private readonly IStoryData _storyData;

        public ObservableObject CurrentViewModel { get; set; }

        public MainViewModel(IStoryData storyData)
        {
            _storyData = storyData;

            CurrentViewModel = new MainPageModel(_storyData);
        }
    }
}
