﻿using FictionHoarder.Core;
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
    class StoriesViewModel : ObservableObject
    {
        private ObservableCollection<Story> _stories;

        public StoriesViewModel(NavigationStore navigationStore)
        {
            //Testing data binding to ListBox
            this.Stories = new ObservableCollection<Story>();
            this.Stories.Add(new Story() { Title = "White", Author = "NeonZangetsu", Chapters = "29", Summary = "A story about life and death.", Command = new NavigateCommand<ReadPageModel>(navigationStore, () => new ReadPageModel(navigationStore), false)});
            this.Stories.Add(new Story() { Title = "Black", Author = "NeonZangetsu", Chapters = "25", Summary = "A story about life and death. Lets add a little more flavor to this summary. Sounds good right?", Command = new NavigateCommand<ReadPageModel>(navigationStore, () => new ReadPageModel(navigationStore), false) });
            this.Stories.Add(new Story() { Title = "Orange", Author = "NeonZangetsu", Chapters = "21", Summary = "Gotta love the color orange. If you don't like it then you're dumb.", Command = new NavigateCommand<ReadPageModel>(navigationStore, () => new ReadPageModel(navigationStore), false) });
            this.Stories.Add(new Story() { Title = "Red", Author = "NeonZangetsu", Chapters = "11", Summary = "A story about life and death.", Command = new NavigateCommand<ReadPageModel>(navigationStore, () => new ReadPageModel(navigationStore), false) });
            this.Stories.Add(new Story() { Title = "Blue", Author = "NeonZangetsu", Chapters = "9", Summary = "A story about life and death.", Command = new NavigateCommand<ReadPageModel>(navigationStore, () => new ReadPageModel(navigationStore), false) });
            this.Stories.Add(new Story() { Title = "Green", Author = "NeonZangetsu", Chapters = "30", Summary = "A story about life and death., " , Command = new NavigateCommand<ReadPageModel>(navigationStore, () => new ReadPageModel(navigationStore), false) });
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

        new public event PropertyChangedEventHandler PropertyChanged;
    }
}
