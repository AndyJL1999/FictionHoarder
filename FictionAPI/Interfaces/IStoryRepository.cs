using FictionAPI.DTOs;
using FictionDataAccessLibrary.Models;
using System.Threading.Tasks;

namespace FictionAPI.Interfaces
{
    public interface IStoryRepository
    {
        Task<IEnumerable<Story>> GetStories(int userId);
        Task InsertStory(AddStoryDto story);
        Task InsertStoryUser(AddStoryUserDto storyUserDto);
        Task UpdateStory(UpdateStoryDto story);

    }
}