using FictionUI_Library.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FictionUI_Library.API
{
    public interface IStoryEndpoint
    {
        Task AddToStoryHistory(int storyId);
        Task<IEnumerable<StoryModel>> GetUserStories();
        Task<IEnumerable<StoryModel>> GetUserStoryHistory();
        StoryModel StoryForCache { get; set; }
    }
}