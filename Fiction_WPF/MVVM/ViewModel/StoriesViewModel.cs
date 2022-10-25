using FictionHoarder.Core;
using FictionHoarder.MVVM.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace FictionHoarder.MVVM.ViewModel
{
    public class StoriesViewModel : ObservableObject
    {
       // IStoryEndpoint _storyEndpoint;
        private ObservableCollection<Story> _stories;
        public ICommand ToReadCommand { get; set; }


        public StoriesViewModel(NavigationStore navigationStore)
        {
            ToReadCommand = new NavigateCommand<ReadPageModel>(navigationStore, () => new ReadPageModel(navigationStore), false);
            //_storyEndpoint = storyEndpoint;
            //Testing data binding to ListBox
            this.Stories = new ObservableCollection<Story>();
            this.Stories.Add(new Story() { Title = "White", Author = "NeonZangetsu", Chapters = "29", Summary = "A story about life and death.", Command = ToReadCommand });
            this.Stories.Add(new Story() { Title = "Black", Author = "NeonZangetsu", Chapters = "25", Summary = "A story about life and death. Lets add a little more flavor to this summary. Sounds good right?", Command = ToReadCommand});
            this.Stories.Add(new Story() { Title = "Orange", Author = "NeonZangetsu", Chapters = "21", Summary = "Gotta love the color orange. If you don't like it then you're dumb.", Command = ToReadCommand });
            this.Stories.Add(new Story() { Title = "Red", Author = "NeonZangetsu", Chapters = "11", Summary = "A story about life and death. Here we go again. I wish things could be simple." , Command = ToReadCommand });
            this.Stories.Add(new Story() { Title = "Blue", Author = "NeonZangetsu", Chapters = "9", Summary = "Blue like the ocean and sky. Nothing like a blue colored thing. Don't you think the same?" , Command = ToReadCommand });
            this.Stories.Add(new Story() { Title = "Green", Author = "NeonZangetsu", Chapters = "30", Summary = "Green grass. Its everywhere. Let's chop it up and make it short. I hate long grass, its annoying. ", Command = ToReadCommand });
        }

        //public async Task LoadStories()
        //{
        //    var storyList = await _storyEndpoint.GetStories();
        //    Stories = new ObservableCollection<StoryModel>(storyList);
        //}

        public ObservableCollection<Story> Stories
        {
            get { return _stories; }
            set
            {
                _stories = value;

                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Stories)));
            }
        }

        new public event PropertyChangedEventHandler PropertyChanged;
    }
}
