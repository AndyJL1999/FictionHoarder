using FictionUI_Library.Models;
using Microsoft.Extensions.Configuration;
using System;
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
            _loggedInUser = loggedInUser;

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
        }

        private void InitializeClient()
        {
            string? api = _config.GetSection("AppSettings:ApiUrl").Value;

            _apiClient = new HttpClient();
            _apiClient.BaseAddress = new Uri(api);
            _apiClient.DefaultRequestHeaders.Accept.Clear();
            _apiClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
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
            _apiClient.DefaultRequestHeaders.Clear();
            _apiClient.DefaultRequestHeaders.Accept.Clear();
            _apiClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            _apiClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {token}");

            using (HttpResponseMessage response = await _apiClient.GetAsync(_apiClient.BaseAddress + "User"))
            {
                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadFromJsonAsync<LoggedInUser>();
                    _loggedInUser.Username = result.Username;
                    _loggedInUser.Email = result.Email;
                    _loggedInUser.Id = result.Id;
                    _loggedInUser.Token = token;
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
