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
        private ObservableCollection<Story> _stories;
        private Story _selectedStory;

        public string Name => "Stories";
        public event EventHandler? ChangeToReadView;

        public StoriesViewModel()
        {
            ChangeToReadView += StoriesViewModel_ChangeToReadView;

            SetStories();
        }

        private void StoriesViewModel_ChangeToReadView(object sender, EventArgs e)
        {
            App.Current.MainWindow.DataContext = new MainViewModel(new ReadPageModel(SelectedStory));
        }

        public ObservableCollection<Story> Stories
        {
            get { return _stories; }
            set
            {
                _stories = value;
                OnPropertyChanged(nameof(Stories));
            }
        }

        public Story SelectedStory 
        {
            get { return _selectedStory; }
            set
            {
                _selectedStory = value;
                OnPropertyChanged(nameof(SelectedStory));
                ChangeToReadView?.Invoke(this, new EventArgs());
            } 
        }

        public async void SetStories()
        {
            //var response = await _storyData.GetStories();

            //Stories = new ObservableCollection<Story>(response);
        }
    }
}
