using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace FictionDataAccessLibrary.Models
{
    public class Story : INotifyPropertyChanged
    {
        private readonly int _id;
        private string _title = string.Empty;
        private string _author = string.Empty ;
        private string _chapters = string.Empty;
        private string _summary = string.Empty;


        public int ID { get; }

        public string Title
        {
            get { return _title; }
            set
            {
                _title = value;
                INotifyPropertyChanged("Title");
            }

        }
        public string Author
        {
            get { return _author; }
            set
            {
                _author = value;
                INotifyPropertyChanged("Author");
            }
        }
        public string Chapters
        {
            get { return _chapters; }
            set
            {
                _chapters = value;
                INotifyPropertyChanged("Chapters");
            }
        }
        public string Summary
        {
            get { return _summary; }
            set
            {
                _summary = value;
                INotifyPropertyChanged("Summary");
            }
        }

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
