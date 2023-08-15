using FictionUI_Library.Models;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Threading.Tasks;


namespace FictionUI_Library.API
{
    public class ApiHelper : IApiHelper
    {
        private HttpClient _apiClient;
        private readonly IConfiguration _config;
        private ILoggedInUser _loggedInUser;

        public ApiHelper(IConfiguration config, ILoggedInUser loggedInUser)
        {
            _config = config;
            LoggedInUser = loggedInUser;

            InitializeClient();
        }

        public ILoggedInUser LoggedInUser 
        { 
            get { return _loggedInUser; }
            set
            {
                _loggedInUser = value;
            }
        }
        public HttpClient ApiClient 
        { 
            get { return _apiClient; } 
            private set { _apiClient = value; }
        }

        private void InitializeClient()
        {
            string? api = _config.GetSection("AppSettings:ApiUrl").Value;

            ApiClient = new HttpClient();
            ApiClient.BaseAddress = new Uri(api);
            ApiClient.DefaultRequestHeaders.Accept.Clear();
            ApiClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        public async Task<AuthenticatedUser> Authenticate(string email, string password)
        {
            var data = JsonContent.Create(new
            {
                Email = email,
                Password = password
            });

            using (HttpResponseMessage response = await _apiClient.PostAsync(_apiClient.BaseAddress + "Account/login", data))
            {
                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadFromJsonAsync<AuthenticatedUser>();

                    return result;
                }
                else
                {
                    throw new Exception(response.ReasonPhrase);
                }
            }
        }

        public async Task<string> Register(string username, string password, string email)
        {
            var data = JsonContent.Create(new
            {
                Username = username,
                Password = password,
                Email = email
            });

            using (HttpResponseMessage response = await _apiClient.PostAsync(_apiClient.BaseAddress + "Account/register", data))
            {
                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadAsStringAsync();
                }
                else
                {
                    throw new Exception(await response.Content.ReadAsStringAsync());
                }
            }
        }

        public async Task GetUserInfo(string token)
        {
            ApiClient.DefaultRequestHeaders.Clear();
            ApiClient.DefaultRequestHeaders.Accept.Clear();
            ApiClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            ApiClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {token}");

            using (HttpResponseMessage response = await _apiClient.GetAsync(_apiClient.BaseAddress + "User"))
            {
                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadFromJsonAsync<LoggedInUser>();
                    LoggedInUser.Username = result.Username;
                    LoggedInUser.Email = result.Email;
                    LoggedInUser.Id = result.Id;
                    LoggedInUser.Token = token;
                }
                else
                {
                    throw new Exception(response.ReasonPhrase);
                }
            }
        }

        public async Task<string> UpdateUser(string username, string email, string password)
        {
            var data = JsonContent.Create(new
            {
                Username = username,
                Password = password,
                Email = email
            });

            using (HttpResponseMessage response = await _apiClient.PutAsync(_apiClient.BaseAddress + "User", data))
            {
                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadAsStringAsync();
                }
                else
                {
                    throw new Exception(await response.Content.ReadAsStringAsync());
                }
            }
        }

    }
}
