using FictionDataAccessLibrary.Models;
using System.Threading.Tasks;

namespace FictionDataAccessLibrary.Interfaces
{
    public interface IUserData
    {
        Task UpdateUser(User user);
        Task<User> GetUserById(int id);
    }
}