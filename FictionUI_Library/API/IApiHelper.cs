using FictionUI_Library.Models;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace FictionUI_Library.API
{
    public interface IApiHelper
    {
        Task<AuthenticatedUser> Authenticate(string email, string password);
        Task Register(string username, string password, string email);
        Task GetUserInfo(string token);
        ILoggedInUser LoggedInUser { get; set; }
        HttpClient ApiClient { get; }
    }
}