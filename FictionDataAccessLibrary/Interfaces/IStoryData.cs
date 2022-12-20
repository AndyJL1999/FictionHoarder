using FictionDataAccessLibrary.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FictionDataAccessLibrary.Interfaces
{
    public interface IStoryData
    {
        Task DeleteStory(int id);
        Task<IEnumerable<Story>> GetStoriesForUser(int userId);
        Task<Story> GetStory(string title, string author, string epubFile);
        Task InsertStory(int userId, Story story);
        Task UpdateStory(Story story);
    }
}