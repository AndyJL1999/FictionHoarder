using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FictionAPI.DTOs
{
    public class AddStoryDto
    {
        public string Title { get; set; }
        public string Author { get; set; }
        public string Chapters { get; set; }
        public string Summary { get; set; }
        public string EpubFile { get; set; }
    }
}
