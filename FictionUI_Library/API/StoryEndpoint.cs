using FictionUI_Library.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Options;

namespace FictionUI_Library.API
{
    public class StoryEndpoint : IStoryEndpoint
    {
        private readonly IApiHelper _apiHelper;
        private readonly IMemoryCache _memoryCache;
        private readonly string _storyCacheKey = "Stories";
        private readonly string _historyCacheKey = "History";

        public StoryModel StoryForCache { get; set; }

        public StoryEndpoint(IApiHelper apiHelper, IMemoryCache memoryCache)
        {
            _apiHelper = apiHelper;
            _memoryCache = memoryCache;
        }

        public async Task<IEnumerable<StoryModel>> GetUserStories()
        {
            IEnumerable<StoryModel> output;

            output = _memoryCache.Get<IEnumerable<StoryModel>>(_storyCacheKey);

            if(output is null)
            {
                using (HttpResponseMessage response = await _apiHelper.ApiClient.GetAsync(_apiHelper.ApiClient.BaseAddress + "Story"))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        output = await response.Content.ReadFromJsonAsync<IEnumerable<StoryModel>>();

                        _memoryCache.Set(_storyCacheKey, output, new MemoryCacheEntryOptions
                        {
                            SlidingExpiration = TimeSpan.FromHours(1),
                            AbsoluteExpiration = DateTime.UtcNow.AddDays(1)
                        });

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
            output = _memoryCache.Get<List<StoryModel>>(_historyCacheKey);

            if (output is null)
            {
                using (HttpResponseMessage response = await _apiHelper.ApiClient.GetAsync(_apiHelper.ApiClient.BaseAddress + "Story/History"))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        output = await response.Content.ReadFromJsonAsync<List<StoryModel>>();

                        _memoryCache.Set(_historyCacheKey, output, new MemoryCacheEntryOptions
                        {
                            SlidingExpiration = TimeSpan.FromHours(1),
                            AbsoluteExpiration = DateTime.UtcNow.AddDays(1)
                        });

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
    }
}
