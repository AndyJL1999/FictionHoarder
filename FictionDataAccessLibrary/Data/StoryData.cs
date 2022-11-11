using FictionDataAccessLibrary.DbAccess;
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

        public Task<IEnumerable<Story>> GetStories() =>
            _db.LoadData<Story, dynamic>(storedProcedure: "spStory_GetAll", new { });

        public async Task<Story?> GetStory(int id)
        {
            var result = await _db.LoadData<Story, dynamic>(
                storedProcedure: "spStory_Get", new { Id = id });

            return result.FirstOrDefault();
        }

        public Task InsertStory(Story story) =>
            _db.SaveData(storedProcedure: "spStory_Insert", new { story.Title, story.Author, story.Summary, story.Chapters });

        public Task UpdateStory(Story story) =>
            _db.SaveData(storedProcedure: "spStory_Update", story);

        public Task DeleteStory(int id) =>
            _db.SaveData(storedProcedure: "spStory_Delete", new { Id = id });
    }
}
