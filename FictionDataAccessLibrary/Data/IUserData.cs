using FictionDataAccessLibrary.Models;
using System.Threading.Tasks;

namespace FictionDataAccessLibrary.Data
{
    public interface IUserData
    {
        Task UpdateUser(User user);
        Task<User> GetUserById(int id);
    }
}