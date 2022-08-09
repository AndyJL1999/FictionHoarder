using DataAccessLibrary.Data;
using DataAccessLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web;

namespace FictionDataManager.Controllers
{
    [Authorize]
    [RoutePrefix("api/Story")]
    public class StoryController : ApiController
    {
        StoryData data = new StoryData();
        public List<StoryModel> GetAll()
        {
            return data.GetStories();
        }

        public List<StoryModel> GetById(int id)
        {
            return data.GetStory(id);
        }

        public void Post([FromBody] StoryModel value)
        {
            data.InsertStory(value);
        }

        public void Put(int id, [FromBody] StoryModel value)
        {
            data.UpdateStory(id, value);
        }

        public void Delete(int id)
        {
            data.DeleteStory(id);
        }
    }
}
