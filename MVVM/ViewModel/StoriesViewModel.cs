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

        public StoriesViewModel()
        {

            Stories = new ObservableCollection<Story>();
            Stories.Add(new Story() { Title = "White", Author = "NeonZangetsu", Chapters = "29", Summary = "A story about life and death."});
            Stories.Add(new Story() { Title = "Black", Author = "NeonZangetsu", Chapters = "25", Summary = "A story about life and death. Lets add a little more flavor to this summary. Sounds good right?"});
            Stories.Add(new Story() { Title = "Orange", Author = "NeonZangetsu", Chapters = "21", Summary = "Gotta love the color orange. If you don't like it then you're dumb."});
            Stories.Add(new Story() { Title = "Red", Author = "NeonZangetsu", Chapters = "11", Summary = "A story about life and death. Here we go again. I wish things could be simple." });
            Stories.Add(new Story() { Title = "Blue", Author = "NeonZangetsu", Chapters = "9", Summary = "Blue like the ocean and sky. Nothing like a blue colored thing. Don't you think the same?"});
            Stories.Add(new Story() { Title = "Green", Author = "NeonZangetsu", Chapters = "30", Summary = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. " +
                "Ultrices dui sapien eget mi proin. Quam adipiscing vitae proin sagittis nisl rhoncus mattis. Dignissim enim sit amet venenatis urna cursus eget nunc. Id aliquet lectus proin nibh nisl. Sit amet risus nullam eget felis eget nunc lobortis mattis. " +
                "Sem nulla pharetra diam sit amet nisl suscipit adipiscing. Blandit cursus risus at ultrices mi tempus imperdiet. Mus mauris vitae ultricies leo. Augue interdum velit euismod in pellentesque massa placerat duis. Faucibus turpis in eu mi bibendum." });
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
    }
}
