using FictionUI_Library.Models;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FictionUI_Library.API
{
    public interface IApiHelper
    {
        Task<AuthenticatedUser> Authenticate(string email, string password);
        Task GetUserInfo(string token);
        Task<IEnumerable<StoryModel>> GetUserStories();
    }
}