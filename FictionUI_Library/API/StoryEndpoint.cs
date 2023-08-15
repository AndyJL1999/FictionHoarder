using FictionUI_Library.Models;
using System;
using System.Collections.Generic;
using System.Net.Http.Json;
using System.Net.Http;
using System.Threading.Tasks;
using Firebase.Storage;
using System.IO;
using Microsoft.AspNetCore.WebUtilities;

namespace FictionUI_Library.API
{
    public class StoryEndpoint : IStoryEndpoint
    {
        #region ----------Fields----------
        private readonly IApiHelper _apiHelper;
        private string _bucket = "fictionhoarder-9fc9c.appspot.com";
        #endregion

        //Constructor
        public StoryEndpoint(IApiHelper apiHelper)
        {
            _apiHelper = apiHelper;
        }

        //Used to append the cache lists of stories (stories & history)
        public StoryModel StoryForCache { get; set; }

        #region ----------Methods----------
        public async Task<byte[]> GetStoryForReading(int storyId)
        {
            var storyDownload = await new FirebaseStorage(_bucket).Child(storyId + ".epub").GetDownloadUrlAsync();

            using (HttpResponseMessage response = await _apiHelper.ApiClient.GetAsync(storyDownload))
            {
                byte[] output = await response.Content.ReadAsByteArrayAsync();
                
                return output;
            }
        }

        public async Task<IEnumerable<StoryModel>> GetUserStories(bool comingFromSearch = false)
        {
            List<StoryModel> output;

            output = _apiHelper.LoggedInUser.StoryData["Stories"];

            if(output.Count == 0 || comingFromSearch == true)
            {
                using (HttpResponseMessage response = await _apiHelper.ApiClient.GetAsync(_apiHelper.ApiClient.BaseAddress + "Story/GetAllUserStories"))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        output = await response.Content.ReadFromJsonAsync<List<StoryModel>>();
                        _apiHelper.LoggedInUser.StoryData["Stories"] = output;

                        return output;
                    }
                    else
                    {
                        throw new Exception(response.ReasonPhrase);
                    }
                }
            }

            return output;
            
        }

        public async Task<IEnumerable<StoryModel>> GetUserStoryHistory()
        {
            List<StoryModel> output;

            //Get stories from cache if they exist
            output = _apiHelper.LoggedInUser.StoryData["History"];

            if (output.Count == 0)
            {
                using (HttpResponseMessage response = await _apiHelper.ApiClient.GetAsync(_apiHelper.ApiClient.BaseAddress + "Story/History"))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        output = await response.Content.ReadFromJsonAsync<List<StoryModel>>();
                        _apiHelper.LoggedInUser.StoryData["History"] = output;

                        return output;
                    }
                    else
                    {
                        throw new Exception(response.ReasonPhrase);
                    }
                }
            }

            //Find former story position for removal
            var match = output.Find(s => s.Id == StoryForCache.Id);
            output.Remove(match);

            //Re-adding story to the top of the list
            output.Insert(0, StoryForCache);

            return output;
        }

        public async Task AddToStoryHistory(int storyId)
        {
            var content = JsonContent.Create(storyId);

            using (HttpResponseMessage response = await _apiHelper.ApiClient.PostAsync(_apiHelper.ApiClient.BaseAddress + "Story/History", content))
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

        public async Task InsertNewStory(StoryModel story)
        {
            var newStory = (new
            {
                Title = story.Title,
                Author = story.Author,
                Chapters = story.Chapters,
                Summary = story.Summary,
                EpubFile = $"{_bucket}/{story.Title}.epub"
            });

            var content = JsonContent.Create(newStory);

            using (HttpResponseMessage response = await _apiHelper.ApiClient.PostAsync(_apiHelper.ApiClient.BaseAddress + "Story", content))
            {
                if (response.IsSuccessStatusCode)
                {
                    //Check for newly added story and get its id
                    int? storyId = await CheckStory(story);

                    if (storyId != null)
                    {
                        //use the story id as the fileName in Firebase Storage
                        await SendToStorage(story.EpubFile, storyId + ".epub");
                        //update epubfile in database
                        await UpdateStory(storyId, story);
                    }
                }
                else
                {
                    throw new Exception(response.ReasonPhrase);
                }
            }
        }

        public async Task RemoveUserStory(int storyId)
        {
            using (HttpResponseMessage response = await _apiHelper.ApiClient.DeleteAsync(_apiHelper.ApiClient.BaseAddress + $"Story/User/{storyId}"))
            {
                if (response.IsSuccessStatusCode)
                {
                    var output = _apiHelper.LoggedInUser.StoryData["Stories"];

                    StoryModel storyToRemove = output.Find(o => o.Id == storyId);
                    output.Remove(storyToRemove);

                    _apiHelper.LoggedInUser.StoryData["Stories"] = output;

                    Console.WriteLine("Success!");
                }
                else
                {
                    throw new Exception(response.ReasonPhrase);
                }
            }
        }

        public async Task RemoveFromUserStoryHistory(int storyId)
        {
            using (HttpResponseMessage response = await _apiHelper.ApiClient.DeleteAsync(_apiHelper.ApiClient.BaseAddress + $"Story/User/History/{storyId}"))
            {
                if (response.IsSuccessStatusCode)
                {
                    var output = _apiHelper.LoggedInUser.StoryData["History"];

                    StoryModel storyToRemove = output.Find(o => o.Id == storyId);
                    output.Remove(storyToRemove);

                    _apiHelper.LoggedInUser.StoryData["History"] = output;

                    Console.WriteLine("Success!");
                }
                else
                {
                    throw new Exception(response.ReasonPhrase);
                }
            }
        }

        public async Task UpdateStory(int? storyId, StoryModel story)
        {
            var newStory = (new
            {
                Id = (int)storyId,
                Title = story.Title,
                Author = story.Author,
                Chapters = story.Chapters,
                Summary = story.Summary,
                EpubFile = $"{_bucket}/{storyId}.epub"

            });

            var content = JsonContent.Create(newStory);

            using (HttpResponseMessage response = await _apiHelper.ApiClient.PutAsync(_apiHelper.ApiClient.BaseAddress + "Story", content))
            {
                if (response.IsSuccessStatusCode)
                {
                    //Success
                }
                else
                {
                    throw new Exception(response.ReasonPhrase);
                }
            }
        }

        private async Task SendToStorage(string filePath, string fileName)
        {
            using (FileStream fileStream = new FileStream(filePath, FileMode.Open))
            {
                //Add file to firebase storage
                await new FirebaseStorage(_bucket).Child(fileName).PutAsync(fileStream);
            }
        }

        private async Task<int?> CheckStory(StoryModel story)
        {
            var storyForCheck = new Dictionary<string, string>()
            {
                ["Title"] = story.Title,
                ["Author"] = story.Author,
                ["EpubFile"] = $"{_bucket}/{story.Title}.epub"
            };

            var uri = QueryHelpers.AddQueryString(_apiHelper.ApiClient.BaseAddress + $"Story", storyForCheck);

            using (HttpResponseMessage response = await _apiHelper.ApiClient.GetAsync(uri))
            {
                try
                {
                    if (response.IsSuccessStatusCode)
                    {
                        //Get the story and return it's id
                        var result = await response.Content.ReadFromJsonAsync<StoryModel>();

                        return result.Id;
                    }
                    else
                    {
                        throw new Exception(response.ReasonPhrase);
                    }
                }
                catch
                {
                    return null;
                }
            }
        }
        #endregion
    }
}
