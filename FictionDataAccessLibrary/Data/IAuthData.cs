using FictionDataAccessLibrary.DTOs;
using FictionDataAccessLibrary.Models;
using System.Threading.Tasks;

namespace FictionDataAccessLibrary.Data
{
    public interface IAuthData
    {
        Task<User> LoginUser(LoginDto user);
        Task RegisterUser(RegisterDto user);
    }
}