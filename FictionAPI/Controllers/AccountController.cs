using FictionAPI.Data;
using FictionAPI.DTOs;
using FictionDataAccessLibrary.Data;
using FictionDataAccessLibrary.DTOs;
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
        private readonly IAuthRepository _auth;

        public AccountController(IAuthRepository auth)
        {
            _auth = auth;
        }



        [HttpPost("Register")]
        public async Task<ActionResult> Register(RegisterDto user)
        {
            await _auth.Register(new User
            {
                Username = user.Username,
                Email = user.Email,
            }, user.Password);
            
            return Ok();
        }

       [HttpPost("login")]
       public async Task<ActionResult<string>> Login(LoginDto user)
       {
            return Ok(await _auth.Login(user));
       }
    }
}
