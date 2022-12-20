using FictionAPI.Extentions;
using FictionAPI.Interfaces;
using FictionDataAccessLibrary.Data;
using FictionAPI.DTOs;
using FictionDataAccessLibrary.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FictionAPI.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class StoryController : Controller
    {
        private readonly IStoryRepository _storyRepo;

        public StoryController(IStoryRepository storyRepo)
        {
            _storyRepo = storyRepo;
        }

        [HttpGet]
        public async Task<ActionResult<Story>> GetStory([FromQuery]GetStoryDto storyDto)
        {
            return Ok(await _storyRepo.GetStory(storyDto));
        }

        [HttpGet("GetAllUserStories")]
        public async Task<ActionResult<IEnumerable<Story>>> GetStories()
        {
            var id = User.GetUserId();

            var stories = await _storyRepo.GetStories(id); 

            return Ok(stories);
        }

        [HttpGet("History")]
        public async Task<ActionResult<IEnumerable<Story>>> GetHistory()
        {
            var id = User.GetUserId();

            var stories = await _storyRepo.GetHistory(id);

            return Ok(stories);
        }

        [HttpPost]
        public async Task<ActionResult> InsertStoryForUser(AddStoryDto story)
        {
            var id = User.GetUserId();

            await _storyRepo.InsertStory(id, story);
            return Ok();
        }

        [HttpPost("User/{storyId}")]
        public async Task<ActionResult> AddStoryToUser(int storyId)
        {
            int userId = User.GetUserId();
            await _storyRepo.InsertStoryUser(storyId, userId);
            return Ok();
        }

        [HttpPost("History")]
        public async Task<ActionResult> AddToStoryHistory([FromBody]int storyId)
        {
            int userId = User.GetUserId();

            await _storyRepo.InsertIntoHistory(storyId, userId);
            return Ok();
        }

        [HttpPut]
        public async Task<ActionResult> UpdateStory(UpdateStoryDto story)
        {
            await _storyRepo.UpdateStory(story);

            return Ok();
        }

        [HttpDelete("User/{storyId}")]
        public async Task<ActionResult> DeleteStoryFromUser(int storyId)
        {
            int userId = User.GetUserId();
            await _storyRepo.RemoveStoryUser(storyId, userId);
            return Ok();
        }

        [HttpDelete("User/History/{storyId}")]
        public async Task<ActionResult> DeleteStoryFromUserHistory(int storyId)
        {
            int userId = User.GetUserId();
            await _storyRepo.RemoveStoryFromUserHistory(storyId, userId);
            return Ok();
        }
    }
}