using DataAccess.DbAccess;
using DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Data
{
    public class StoryData : IStoryData
    {
        private readonly ISqlDataAccess _db;
        public StoryData(ISqlDataAccess db)
        {
            _db = db;
        }

        public Task<IEnumerable<StoryModel>> GetStories() =>
            _db.LoadData<StoryModel, dynamic>(storedProcedure: "dbo.spStory_GetAll", new { });

        public async Task<StoryModel?> GetStory(int id)
        {
            var results = await _db.LoadData<StoryModel, dynamic>(
                storedProcedure: "dbo.spStory_Get", new { Id = id });

            return results.FirstOrDefault();
        }

        public Task InsertStory(StoryModel story) =>
            _db.SaveData(storedProcedure: "dbo.spStory_Insert", new { story.Title, story.Author, story.Summary });

        public Task UpdateStory(StoryModel story) =>
            _db.SaveData(storedProcedure: "dbo.spStory_Update", story);

        public Task DeleteStory(int id) =>
            _db.SaveData(storedProcedure: "dbo.spStory_Delete", new { Id = id });
    }
}
