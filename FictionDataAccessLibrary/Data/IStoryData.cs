using FictionDataAccessLibrary.DTOs;
using FictionDataAccessLibrary.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FictionDataAccessLibrary.Data
{
    public interface IStoryData
    {
        Task DeleteStory(int id);
        Task<IEnumerable<Story>> GetStoriesForUser(int userId);
        Task<Story?> GetStory(int id);
        Task InsertStory(AddStoryDto story);
        Task UpdateStory(UpdateStoryDto story);
    }
}