using DataAccess.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DataAccess.Data
{
    public interface IStoryData
    {
        Task DeleteStory(int id);
        Task<IEnumerable<StoryModel>> GetStories();
        Task<StoryModel> GetStory(int id);
        Task InsertStory(StoryModel story);
        Task UpdateStory(StoryModel story);
    }
}