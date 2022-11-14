using FictionDataAccessLibrary.Data;
using FictionDataAccessLibrary.DTOs;
using FictionDataAccessLibrary.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FictionAPI.Controllers
{
    //[Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class StoryController : Controller
    {
        private readonly IStoryData _storyData;

        public StoryController(IStoryData storyData)
        {
            _storyData = storyData;
        }

        [HttpGet("{userId}")]
        public async Task<ActionResult<IEnumerable<Story>>> GetStories(int userId)
        {
            return Ok(await _storyData.GetStoriesForUser(userId));
        }

        [HttpPost]
        public async Task<ActionResult> InsertStory(AddStoryDto story)
        {
            await _storyData.InsertStory(story);
            return Ok();
        }

        [HttpPut]
        public async Task<ActionResult> UpdateStory(UpdateStoryDto story)
        {
            await _storyData.UpdateStory(story);

            return Ok();
        }
    }
}