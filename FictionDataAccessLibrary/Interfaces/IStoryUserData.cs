using System.Threading.Tasks;

namespace FictionDataAccessLibrary.Interfaces
{
    public interface IStoryUserData
    {
        Task InsertStoryUser(int storyId, int userId);
    }
}