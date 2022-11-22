using FictionAPI.DTOs;
using FictionDataAccessLibrary.Models;
using System.Threading.Tasks;

namespace FictionAPI.Interfaces
{
    public interface IStoryRepository
    {
        Task<IEnumerable<Story>> GetStories(int userId);
        Task<IEnumerable<Story>> GetHistory(int userId);
        Task InsertStory(AddStoryDto story);
        Task InsertStoryUser(int storyId, int userId);
        Task InsertIntoHistory(int storyId, int userId);
        Task UpdateStory(UpdateStoryDto story);

    }
}