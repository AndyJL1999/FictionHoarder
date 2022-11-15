using FictionDataAccessLibrary.Models;
using System.Threading.Tasks;

namespace FictionDataAccessLibrary.Data
{
    public interface IAuthData
    {
        Task<User> LoginUser(User user);
        Task RegisterUser(User user);

        Task<User> GetUserByNameOrEmail(string username);
    }
}