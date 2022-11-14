using FictionDataAccessLibrary.Data;
using FictionDataAccessLibrary.DTOs;
using FictionDataAccessLibrary.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace FictionAPI.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class AccountController : ControllerBase
    {
        private readonly IAuthData _authData;

        public AccountController(IAuthData authData)
        {
            _authData = authData;
        }



        [HttpPost("Register")]
        public async Task<ActionResult> Register(RegisterDto user)
        {
            await _authData.RegisterUser(user);
            return Ok();
        }

       [HttpPost("login")]
       public async Task<ActionResult<User>> Login(LoginDto user)
       {
            var returnedUser = await _authData.LoginUser(user);

            if (returnedUser == null)
                return BadRequest("User does not exist")
;
            return Ok();
       }
    }
}
