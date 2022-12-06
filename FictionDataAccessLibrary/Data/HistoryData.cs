using FictionDataAccessLibrary.DbAccess;
using FictionDataAccessLibrary.Interfaces;
using FictionDataAccessLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FictionDataAccessLibrary.Data
{
    public class HistoryData : IHistoryData
    {
        private readonly ISqlDataAccess _db;

        public HistoryData(ISqlDataAccess db)
        {
            _db = db;
        }

        public Task<IEnumerable<Story>> GetHistoryForUser(int userId) =>
             _db.LoadData<Story, dynamic>(storedProcedure: "spUserStoryHistory_GetHistory", new { UserId = userId });

        public Task InsertStoryIntoHistory(int viewedStoryId, int userId) =>
            _db.SaveData(storedProcedure: "spUserStoryHistory_Insert", new {ViewedStoryId = viewedStoryId, UserId = userId, TimeViewed = DateTime.UtcNow });

        public Task DeleteStoryFromHistory(int viewedStoryId, int userId) =>
            _db.SaveData(storedProcedure: "spUserStoryHistory_Delete", new { StoryId = viewedStoryId, UserId = userId });

    }
}
