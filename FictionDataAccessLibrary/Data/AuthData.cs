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
    public class AuthData : IAuthData
    {
        private readonly ISqlDataAccess _db;

        public AuthData(ISqlDataAccess db)
        {
            _db = db;
        }

        public async Task<User> LoginUser(User user)
        {
            var result = await _db.LoadData<User, dynamic>(
                storedProcedure: "spUser_UserLogin", new { Email = user.Email, PasswordHash = user.PasswordHash, PasswordSalt = user.PasswordSalt });

            return result.FirstOrDefault();
        }

        public Task RegisterUser(User user) =>
            _db.SaveData(storedProcedure: "spUser_UserRegister", new { user.Username, user.Email, user.PasswordHash, user.PasswordSalt });

        public async Task<User> GetUserByEmail(string email)
        {
            var result = await _db.LoadData<User, dynamic>(
                storedProcedure: "spUser_GetUserByEmail", new { Email = email });

            return result.FirstOrDefault();
        }

        public async Task<User> GetUserByUsername(string username)
        {
            var result = await _db.LoadData<User, dynamic>(
                storedProcedure: "spUser_GetUserByUsername", new { Username = username });

            return result.FirstOrDefault();
        }

    }
}
