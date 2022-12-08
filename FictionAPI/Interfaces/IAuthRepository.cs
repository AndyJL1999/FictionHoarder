using FictionAPI.DTOs;
using FictionDataAccessLibrary.Models;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace FictionAPI.Interfaces
{
    public interface IAuthRepository
    {
        Task<UserDto> Login(LoginDto login);
        Task<string> Register(User user, string password);
        void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt);
        bool ValidateEmail(string email);
    }
}