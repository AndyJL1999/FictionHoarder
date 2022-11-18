using FictionAPI.DTOs;
using FictionDataAccessLibrary.Models;

namespace FictionAPI.Interfaces
{
    public interface IStoryRepository
    {
        Task<IEnumerable<Story>> GetStories(int userId);
        Task InsertStory(AddStoryDto story);
        Task UpdateStory(UpdateStoryDto story);
    }
}