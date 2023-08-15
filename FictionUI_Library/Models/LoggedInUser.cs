using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FictionUI_Library.Models
{
    public class LoggedInUser : ILoggedInUser
    {
        public string Token { get; set; }
        public int Id { get; set; }
        public string Username { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public Dictionary<string, List<StoryModel>> StoryData { get; } = new Dictionary<string, List<StoryModel>>
        {
            { "Stories", new List<StoryModel>() },
            { "History", new List<StoryModel>() }
        };

        public void ClearUser()
        {
            StoryData["Stories"].Clear();
            StoryData["History"].Clear();
            Token = string.Empty;
            Id = 0;
            Username = string.Empty;
            Email = string.Empty;
        }
    }
}
