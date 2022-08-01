using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.Models
{
    public class StoryModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public string Summary { get; set; }
    }
}
