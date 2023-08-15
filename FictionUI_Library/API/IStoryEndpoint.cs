using FictionUI_Library.Models;
using Microsoft.Extensions.Caching.Memory;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace FictionUI_Library.API
{
    public interface IStoryEndpoint
    {
        Task<byte[]> GetStoryForReading(int storyId);
        Task AddToStoryHistory(int storyId);
        Task<IEnumerable<StoryModel>> GetUserStories(bool comingFromSearch);
        Task<IEnumerable<StoryModel>> GetUserStoryHistory();
        Task InsertNewStory(StoryModel story);
        Task RemoveUserStory(int storyId);
        Task RemoveFromUserStoryHistory(int storyId);
        StoryModel StoryForCache { get; set; }
    }
}