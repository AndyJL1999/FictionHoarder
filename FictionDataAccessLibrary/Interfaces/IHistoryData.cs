using FictionDataAccessLibrary.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FictionDataAccessLibrary.Interfaces
{
    public interface IHistoryData
    {
        Task<IEnumerable<Story>> GetHistoryForUser(int userId);
        Task InsertStoryIntoHistory(int viewedStoryId, int userId);
    }
}