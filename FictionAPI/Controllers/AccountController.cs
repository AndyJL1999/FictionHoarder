using FictionAPI.DTOs;
using FictionAPI.Interfaces;
using FictionDataAccessLibrary.Data;
using FictionAPI.DTOs;
using FictionDataAccessLibrary.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace FictionAPI.Controllers
{
    //[Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class AccountController : ControllerBase
    {
        private readonly IAuthRepository _authRepo;

        public AccountController(IAuthRepository auth)
        {
            _authRepo = auth;
        }



        [HttpPost("register")]
        public async Task<ActionResult> Register(RegisterDto user)
        {
            await _authRepo.Register(new User
            {
                Username = user.Username,
                Email = user.Email,
            }, user.Password);
            
            return Ok();
        }

       [HttpPost("login")]
       public async Task<ActionResult<UserDto>> Login(LoginDto user)
       {
            var userLogin = await _authRepo.Login(user);

            if (userLogin == null)
                return Unauthorized("Invalid user");

            return Ok(userLogin);
       }
    }
}
