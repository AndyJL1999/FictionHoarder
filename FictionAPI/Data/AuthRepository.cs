﻿using AutoMapper;
using FictionAPI.DTOs;
using FictionAPI.Interfaces;
using FictionAPI.DTOs;
using FictionDataAccessLibrary.Models;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using FictionDataAccessLibrary.Interfaces;
using System.Net.Mail;
using System.Text.RegularExpressions;

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

        public async Task<string> Register(User registerUser, string password)
        {
            if (registerUser.Username.IsNullOrEmpty())
            {
                return "Please enter a username";
            }

            if ((ValidateEmail(registerUser.Email) == false) && (password.Length < 8))
            {
                return "Invalid email and password";
            }

            if (ValidateEmail(registerUser.Email) == false)
            {
                return "Invalid email";
            }

            if (password.Length < 8)
            {
                return "Password must be at least 8 characters";
            }

            if(await DoesUserExist(registerUser.Username))
            {
                return "User already exists";
            }

            CreatePasswordHash(password, out byte[] passwordHash, out byte[] passwordSalt);

            registerUser.PasswordHash = passwordHash;
            registerUser.PasswordSalt = passwordSalt;

            await _authData.RegisterUser(registerUser);

            return "User has been registered";
        }

        public async Task<UserDto> Login(LoginDto loginDto)
        {
            var user = await _authData.GetUserByEmail(loginDto.Email);


            if (user == null || !VerifyPasswordHash(loginDto.Password, user.PasswordHash, user.PasswordSalt))
            {
                return null;
            }

            return new UserDto
            {
                Username = user.Username,
                Token = CreateToken(user)
            };
        }

        public void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
            }
        }

        public bool ValidateEmail(string email)
        {
            Regex rx = new Regex(@"^[-!#$%&'*+/0-9=?A-Z^_a-z{|}~](\.?[-!#$%&'*+/0-9=?A-Z^_a-z{|}~])*@[a-zA-Z](-?[a-zA-Z0-9])*(\.[a-zA-Z](-?[a-zA-Z0-9])*)+$");

            return rx.IsMatch(email);
        }

        private bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512(passwordSalt))
            {
                var computeHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));

                bool t = passwordHash == computeHash;


                return computeHash.SequenceEqual(passwordHash);
            }
        }

        private async Task<bool> DoesUserExist(string username)
        {
            var user = await _authData.GetUserByUsername(username);

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
