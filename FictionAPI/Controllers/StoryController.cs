using FictionDataAccessLibrary.Data;
using FictionDataAccessLibrary.Models;
using Microsoft.AspNetCore.Mvc;

namespace FictionAPI.Controllers
{
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
    }
}