using FictionDataAccessLibrary.DbAccess;
using FictionDataAccessLibrary.DTOs;
using FictionDataAccessLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FictionDataAccessLibrary.Data
{
    public class StoryData : IStoryData
    {
        private readonly ISqlDataAccess _db;

        public StoryData(ISqlDataAccess db)
        {
            _db = db;
        }

        public Task<IEnumerable<Story>> GetStoriesForUser(int userId) =>
            _db.LoadData<Story, dynamic>(storedProcedure: "spStory_GetAll", new { UserId = userId });

        public async Task<Story?> GetStory(int id)
        {
            var result = await _db.LoadData<Story, dynamic>(
                storedProcedure: "spStory_Get", new { Id = id });

            return result.FirstOrDefault();
        }

        public Task InsertStory(AddStoryDto story) =>
            _db.SaveData(storedProcedure: "spStory_Insert", new { story.UserId, story.Title, story.Author, story.Summary, story.Chapters });

        public Task UpdateStory(UpdateStoryDto story) => 
            _db.SaveData(storedProcedure: "spStory_Update", new { story.Id, story.Title, story.Author, story.Summary, story.Chapters});

        public Task DeleteStory(int id) =>
            _db.SaveData(storedProcedure: "spStory_Delete", new { Id = id });
    }
}
