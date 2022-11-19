using AutoMapper;
using FictionAPI.Interfaces;
using FictionAPI.DTOs;
using FictionDataAccessLibrary.Models;
using Microsoft.AspNetCore.Mvc;
using FictionDataAccessLibrary.Interfaces;

namespace FictionAPI.Data
{
    public class StoryRepository : IStoryRepository
    {
        private readonly IStoryData _storyData;
        private readonly IStoryUserData _storyUserData;
        private readonly IMapper _mapper;
        private readonly IConfiguration _config;

        public StoryRepository(IStoryData storyData, IStoryUserData storyUserData, IMapper mapper, IConfiguration config)
        {
            _storyData = storyData;
            _storyUserData = storyUserData;
            _mapper = mapper;
            _config = config;
        }

        public async Task<IEnumerable<Story>> GetStories(int userId)
        {
            return await _storyData.GetStoriesForUser(userId);
        }

        public async Task InsertStory(AddStoryDto storyDto)
        {
            var story = _mapper.Map<AddStoryDto, Story>(storyDto);
            await _storyData.InsertStory(story);
        }

        public async Task InsertStoryUser(AddStoryUserDto storyUserDto)
        {
            await _storyUserData.InsertStoryUser(storyUserDto.StoryId, storyUserDto.UserId);
        }

        public async Task UpdateStory(UpdateStoryDto storyDto)
        {
            var story = _mapper.Map<UpdateStoryDto, Story>(storyDto);
            await _storyData.UpdateStory(story);
        }
    }
}
