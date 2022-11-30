using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace FictionDataAccessLibrary.Models
{
    public class Story
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public string Chapters { get; set; }
        public string Summary { get; set; }
        public string EpubFile { get; set; }
    }
}
