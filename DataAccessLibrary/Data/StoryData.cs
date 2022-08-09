using DataAccessLibrary.DbAccess;
using DataAccessLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLibrary.Data
{
    public class StoryData
    {
        SqlDataAccess sql = new SqlDataAccess();

        public List<StoryModel> GetStories() =>
            sql.LoadData<StoryModel, dynamic>(storedProcedure: "dbo.spStory_GetAll", new { }, "Fiction_DB");

        public List<StoryModel> GetStory(int id) =>
            sql.LoadData<StoryModel, dynamic>(storedProcedure: "dbo.spStory_Get", new { Id = id }, "Fiction_DB");

        public void InsertStory(StoryModel story) =>
            sql.SaveData(storedProcedure: "dbo.spStory_Insert", new { story.Id, story.Title, story.Author, story.Summary }, "Fiction_DB");

        public void UpdateStory(int id, StoryModel story) =>
            sql.SaveData(storedProcedure: "dbo.spStory_Update", new {Id = id, story.Title, story.Author, story.Summary}, "Fiction_DB");
        
        public void DeleteStory(int id) =>
            sql.SaveData(storedProcedure: "dbo.spStory_Delete", new { Id = id }, "Fiction_DB");
    }
}
