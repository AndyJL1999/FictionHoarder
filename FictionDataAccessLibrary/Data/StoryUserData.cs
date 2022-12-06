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
    public class StoryUserData : IStoryUserData
    {
        private readonly ISqlDataAccess _db;

        public StoryUserData(ISqlDataAccess db)
        {
            _db = db;
        }   

        public Task InsertStoryUser(int storyId, int userId) =>
            _db.SaveData(storedProcedure: "spStoryUser_InsertRelationship", new { StoryId = storyId, UserId = userId });

        public Task DeleteStoryUser(int storyId, int userId) =>
            _db.SaveData(storedProcedure: "spStoryUser_DeleteRelationship", new { StoryId = storyId, UserId = userId });
    }
}
