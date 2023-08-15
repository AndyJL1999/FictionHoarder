using System.Collections.Generic;

namespace FictionUI_Library.Models
{
    public interface ILoggedInUser
    {
        string Email { get; set; }
        int Id { get; set; }
        string Token { get; set; }
        string Username { get; set; }
        Dictionary<string, List<StoryModel>> StoryData { get; }
        void ClearUser();
    }
}