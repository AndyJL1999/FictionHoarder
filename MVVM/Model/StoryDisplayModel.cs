using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FictionHoarderWPF.MVVM.Model
{
    public class StoryDisplayModel : INotifyPropertyChanged
    {
        private readonly int _id;
        private readonly int _userId;
        private string _title = string.Empty;
        private string _author = string.Empty;
        private string _chapters = string.Empty;
        private string _summary = string.Empty;


        public int Id { get; set; }
        public int UserId { get; set; }

        public string Title
        {
            get { return _title; }
            set
            {
                _title = value;
                INotifyPropertyChanged(nameof(Title));
            }

        }
        public string Author
        {
            get { return _author; }
            set
            {
                _author = value;
                INotifyPropertyChanged(nameof(Author));
            }
        }
        public string Chapters
        {
            get { return _chapters; }
            set
            {
                _chapters = value;
                INotifyPropertyChanged(nameof(Chapters));
            }
        }
        public string Summary
        {
            get { return _summary; }
            set
            {
                _summary = value;
                INotifyPropertyChanged(nameof(Summary));
            }
        }

        public string EpubFile { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

        private void INotifyPropertyChanged(string v)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(v));
            }
        }
    }
}
