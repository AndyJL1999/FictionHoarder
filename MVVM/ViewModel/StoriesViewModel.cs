using FictionHoarderWPF.Core;
using FictionDataAccessLibrary.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using FictionHoarderWPF.Core.Interfaces;
using FictionDataAccessLibrary.Data;

namespace FictionHoarderWPF.MVVM.ViewModel
{
    public class StoriesViewModel : ObservableObject, IViewModel
    {
        private readonly IStoryData _storyData;
        private ObservableCollection<Story> _stories;

        public StoriesViewModel(IStoryData storyData)
        {
            _storyData = storyData;
            SetStories();
        }

        public ObservableCollection<Story> Stories
        {
            get { return _stories; }
            set
            {
                _stories = value;

                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Stories)));
            }
        }

        public string Name => "Stories";

        new public event PropertyChangedEventHandler PropertyChanged;

        public async void SetStories()
        {
            var response = await _storyData.GetStories();

            Stories = new ObservableCollection<Story>(response);
        }
    }
}
