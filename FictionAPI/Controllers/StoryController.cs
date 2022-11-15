using FictionAPI.Data;
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
        private readonly IStoryRepository _storyRepo;

        public StoryController(IStoryRepository storyRepo)
        {
            _storyRepo = storyRepo;
        }

        [HttpGet("{userId}")]
        public async Task<ActionResult<IEnumerable<Story>>> GetStories(int userId)
        {
            return Ok(await _storyRepo.GetStories(userId));
        }

        [HttpPost]
        public async Task<ActionResult> InsertStory(AddStoryDto story)
        {
            await _storyRepo.InsertStory(story);
            return Ok();
        }

        [HttpPut]
        public async Task<ActionResult> UpdateStory(UpdateStoryDto story)
        {
            await _storyRepo.UpdateStory(story);

            return Ok();
        }
    }
}