using FictionDataAccessLibrary.DTOs;
using FictionDataAccessLibrary.Models;

namespace FictionAPI.Data
{
    public interface IStoryRepository
    {
        Task<IEnumerable<Story>> GetStories(int userId);
        Task InsertStory(AddStoryDto story);
        Task UpdateStory(UpdateStoryDto story);
    }
}