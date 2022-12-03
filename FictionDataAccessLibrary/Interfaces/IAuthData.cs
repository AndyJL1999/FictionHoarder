using FictionDataAccessLibrary.Models;
using System.Threading.Tasks;

namespace FictionDataAccessLibrary.Interfaces
{
    public interface IAuthData
    {
        Task<User> LoginUser(User user);
        Task RegisterUser(User user);

        Task<User> GetUserByEmail(string email);
        Task<User> GetUserByUsername(string username);
    }
}