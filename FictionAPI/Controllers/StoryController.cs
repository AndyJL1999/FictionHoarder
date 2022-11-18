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
        public async Task<ActionResult<IEnumerable<Story>>> GetStories()
        {
            var id = User.GetUserId();

            var stories = await _storyRepo.GetStories(id); 

            return Ok(stories);
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