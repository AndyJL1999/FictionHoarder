﻿using FictionAPI.DTOs;
using FictionDataAccessLibrary.Models;
using System.Threading.Tasks;

namespace FictionAPI.Interfaces
{
    public interface IStoryRepository
    {
        Task<Story> GetStory(GetStoryDto storyDto);
        Task<IEnumerable<Story>> GetStories(int userId);
        Task<IEnumerable<Story>> GetHistory(int userId);
        Task InsertStory(int userId, AddStoryDto story);
        Task InsertStoryUser(int storyId, int userId);
        Task InsertIntoHistory(int storyId, int userId);
        Task UpdateStory(UpdateStoryDto story);
        Task RemoveStoryUser(int storyId, int userId);
        Task RemoveStoryFromUserHistory(int storyId, int userId);

    }
}