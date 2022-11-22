using FictionAPI.DTOs;
using FictionUI_Library.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using System.Windows.Markup;

namespace FictionUI_Library.API
{
    public class ApiHelper : IApiHelper
    {
        private HttpClient _apiClient;
        private readonly ILoggedInUser _loggedInUser;

        public ApiHelper(ILoggedInUser loggedInUser)
        {
            InitializeClient();
            _loggedInUser = loggedInUser;
        }

        public string User { get => _loggedInUser.Username; }

        private void InitializeClient()
        {
            string api = ConfigurationManager.AppSettings["ApiUrl"];

            _apiClient = new HttpClient();
            _apiClient.BaseAddress = new Uri(api);
            _apiClient.DefaultRequestHeaders.Accept.Clear();
            _apiClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        public async Task<AuthenticatedUser> Authenticate(string email, string password)
        {
            var data = JsonContent.Create(new LoginDto
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

        public async Task Register(string username, string password, string email)
        {
            var data = JsonContent.Create(new RegisterDto
            {
                Username = username,
                Password = password,
                Email = email
            });

            using (HttpResponseMessage response = await _apiClient.PostAsync(_apiClient.BaseAddress + "Account/register", data))
            {
                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadAsStringAsync();

                    //return result;
                }
                else
                {
                    throw new Exception(response.ReasonPhrase);
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

        public async Task<IEnumerable<StoryModel>> GetUserStories()
        {
            using (HttpResponseMessage response = await _apiClient.GetAsync(_apiClient.BaseAddress + "Story"))
            {
                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadFromJsonAsync<IEnumerable<StoryModel>>();

                    return result;
                }
                else
                {
                    throw new Exception(response.ReasonPhrase);
                }
            }
        }

        public async Task<IEnumerable<StoryModel>> GetUserStoryHistory()
        {
            using (HttpResponseMessage response = await _apiClient.GetAsync(_apiClient.BaseAddress + "Story/History"))
            {
                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadFromJsonAsync<IEnumerable<StoryModel>>();

                    return result;
                }
                else
                {
                    throw new Exception(response.ReasonPhrase);
                }
            }
        }

        public async Task AddToStoryHistory(int storyId)
        {
            var content = JsonContent.Create(storyId);

            using (HttpResponseMessage response = await _apiClient.PostAsync(_apiClient.BaseAddress + "Story/History", content))
            {
                if (response.IsSuccessStatusCode)
                {
                    Console.WriteLine("Success!");
                }
                else
                {
                    throw new Exception(response.ReasonPhrase);
                }
            }
        }

    }
}
