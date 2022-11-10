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
        private string _title;
        private string _author;
        private string _chapters;
        private string _summary;
        private ICommand _command;
        public int ID { get; }

        public ICommand Command
        {
            get { return _command; }
            set
            {
                _command = value;
                INotifyPropertyChanged("Command");
            }
        }
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
