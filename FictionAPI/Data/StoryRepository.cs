using AutoMapper;
using FictionAPI.Interfaces;
using FictionAPI.DTOs;
using FictionDataAccessLibrary.Models;
using Microsoft.AspNetCore.Mvc;
using FictionDataAccessLibrary.Interfaces;
using FictionAPI.Extentions;

namespace FictionAPI.Data
{
    public class StoryRepository : IStoryRepository
    {
        private readonly IStoryData _storyData;
        private readonly IStoryUserData _storyUserData;
        private readonly IHistoryData _historyData;
        private readonly IMapper _mapper;
        private readonly IConfiguration _config;

        public StoryRepository(IStoryData storyData, IStoryUserData storyUserData, IHistoryData historyData,
            IMapper mapper, IConfiguration config)
        {
            _storyData = storyData;
            _storyUserData = storyUserData;
            _historyData = historyData;
            _mapper = mapper;
            _config = config;
        }

        public async Task<IEnumerable<Story>> GetStories(int userId)
        {
            return await _storyData.GetStoriesForUser(userId);
        }

        public async Task<IEnumerable<Story>> GetHistory(int userId)
        {
            return await _historyData.GetHistoryForUser(userId);
        }

        public async Task InsertStory(int userId, AddStoryDto storyDto)
        {
            var story = _mapper.Map<AddStoryDto, Story>(storyDto);
            await _storyData.InsertStory(userId, story);
        }

        public async Task InsertStoryUser(int storyId, int userId)
        {
            await _storyUserData.InsertStoryUser(storyId, userId);
        }

        public async Task InsertIntoHistory(int storyId, int userId)
        {
            await _historyData.InsertStoryIntoHistory(storyId, userId);
        }

        public async Task UpdateStory(UpdateStoryDto storyDto)
        {
            var story = _mapper.Map<UpdateStoryDto, Story>(storyDto);
            await _storyData.UpdateStory(story);
        }
    }
}
