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
    public class AuthData : IAuthData
    {
        private readonly ISqlDataAccess _db;

        public AuthData(ISqlDataAccess db)
        {
            _db = db;
        }

        public async Task<User> LoginUser(LoginDto user)
        {
            var result = await _db.LoadData<User, dynamic>(
                storedProcedure: "spUser_UserLogin", new { Email = user.Email, Password = user.Password });

            return result.FirstOrDefault();
        }

        public Task RegisterUser(RegisterDto user) =>
            _db.SaveData(storedProcedure: "spUser_RegisterUser", new { user.Username, user.Password, user.Email });

    }
}
