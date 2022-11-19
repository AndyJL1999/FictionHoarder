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
    public class UserData : IUserData
    {
        private readonly ISqlDataAccess _db;

        public UserData(ISqlDataAccess db)
        {
            _db = db;
        }

        public async Task<User> GetUserById(int id)
        {
            var result = await _db.LoadData<User, dynamic>(storedProcedure: "spUser_GetUserById", new { Id = id });

            return result.FirstOrDefault();
        }
            

        public Task UpdateUser(User user) =>
            _db.SaveData(storedProcedure: "spUser_UpdateUser", new { user.Id, user.Username, user.PasswordHash, user.PasswordSalt, user.Email });

    }
}
