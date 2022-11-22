using FictionUI_Library.Models;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FictionUI_Library.API
{
    public interface IApiHelper
    {
        Task<AuthenticatedUser> Authenticate(string email, string password);
        Task Register(string username, string password, string email);
        Task GetUserInfo(string token);
        Task<IEnumerable<StoryModel>> GetUserStories();
        Task<IEnumerable<StoryModel>> GetUserStoryHistory();
        Task AddToStoryHistory(int storyId);
        string User { get; }
    }
}