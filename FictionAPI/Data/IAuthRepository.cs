using FictionDataAccessLibrary.DTOs;
using FictionDataAccessLibrary.Models;

namespace FictionAPI.Data
{
    public interface IAuthRepository
    {
        Task<string> Login(LoginDto login);
        Task<string> Register(User user, string password);
    }
}