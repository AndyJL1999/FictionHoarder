using AutoMapper;
using FictionDataAccessLibrary.Data;
using FictionDataAccessLibrary.DTOs;
using FictionDataAccessLibrary.Models;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace FictionAPI.Data
{
    public class AuthRepository : IAuthRepository
    {
        private readonly IAuthData _authData;
        private readonly IMapper _mapper;
        private readonly IConfiguration _config;

        public AuthRepository(IAuthData authData, IMapper mapper, IConfiguration config)
        {
            _authData = authData;
            _mapper = mapper;
            _config = config;
        }

        public async Task<string> Register(User register, string password)
        {
            if(await DoesUserExist(register.Username))
            {
                return "User already exists";
            }

            CreatePasswordHash(password, out byte[] passwordHash, out byte[] passwordSalt);

            register.PasswordHash = passwordHash;
            register.PasswordSalt = passwordSalt;

            await _authData.RegisterUser(register);

            return "User has been registered";
        }

        public async Task<string> Login(LoginDto loginDto)
        {
            var user = await _authData.GetUserByNameOrEmail(loginDto.Email);

            if (user == null)
            {
                return "User does not exist";
            }
            else if(!VerifyPasswordHash(loginDto.Password, user.PasswordHash, user.PasswordSalt))
            {
                return "Wrong Password!";
            }

            _mapper.Map<LoginDto>(user);

            return "Success! You are logged in.";
        }

        private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
            }
        }

        private bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            using (
                var hmac = new HMACSHA512(passwordSalt))
            {
                var computeHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));

                bool t = passwordHash == computeHash;


                return computeHash.SequenceEqual(passwordHash);
            }
        }

        private async Task<bool> DoesUserExist(string username)
        {
            var user = await _authData.GetUserByNameOrEmail(username);

            if (user != null)
            {
                return true;
            }

            return false;
        }

        private string CreateToken(User user)
        {
            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.NameId, user.Id.ToString()),
                new Claim(JwtRegisteredClaimNames.UniqueName, user.Username)
            };

            SymmetricSecurityKey key = new SymmetricSecurityKey(Encoding.UTF8
                .GetBytes(_config.GetSection("AppSettings:Token").Value));

            SigningCredentials credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            SecurityTokenDescriptor tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddDays(1),
                SigningCredentials = credentials
            };

            var tokenHandler = new JwtSecurityTokenHandler();

            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }
    }
}
