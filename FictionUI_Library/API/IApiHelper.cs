﻿using FictionUI_Library.Models;
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
        Task<string> Register(string username, string password, string email);
        Task GetUserInfo(string token);
        Task<string> UpdateUser(string username, string email, string password);
        ILoggedInUser LoggedInUser { get; set; }
        HttpClient ApiClient { get; }
    }
}