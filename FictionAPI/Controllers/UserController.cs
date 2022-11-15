using FictionAPI.DTOs;
using FictionAPI.Extentions;
using FictionAPI.Interfaces;
using FictionDataAccessLibrary.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FictionAPI.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository _userRepo;

        public UserController(IUserRepository userRepo)
        {
            _userRepo = userRepo;
        }

        [HttpGet]
        public async Task<ActionResult<User>> GetUser()
        {
            var id = User.GetUserId();

            var user = await _userRepo.GetUserByIdAsync(id);

            return Ok(user);
        }

        [HttpPut]
        public async Task<ActionResult> UpdateUser(UpdateUserDto user)
        {
            var id = User.GetUserId();
            await _userRepo.UpdateUser(user, id);
            return Ok();
        }

    }
}
