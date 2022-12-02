using FictionUI_Library.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FictionUI_Library.API
{
    public interface IStoryEndpoint
    {
        Task AddToStoryHistory(int storyId);
        Task<IEnumerable<StoryModel>> GetUserStories(bool comingFromSearch);
        Task<IEnumerable<StoryModel>> GetUserStoryHistory();
        Task InsertNewStory(StoryModel story);
        StoryModel StoryForCache { get; set; }
    }
}