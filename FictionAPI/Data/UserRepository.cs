using FictionAPI.DTOs;
using FictionAPI.Interfaces;
using FictionDataAccessLibrary.Interfaces;
using FictionDataAccessLibrary.Models;
using Microsoft.AspNetCore.SignalR;
using Microsoft.IdentityModel.Tokens;

namespace FictionAPI.Data
{
    public class UserRepository : IUserRepository
    {
        private readonly IUserData _userData;
        private readonly IAuthRepository _authRepo;

        public UserRepository(IUserData userData, IAuthRepository authRepo)
        {
            _userData = userData;
            _authRepo = authRepo;
        }

        public async Task<User> GetUserByIdAsync(int id)
        {
            return await _userData.GetUserById(id);
        }

        public async Task<string> UpdateUser(UpdateUserDto userUpdate, int userId)
        {
            if(userUpdate.Username.IsNullOrEmpty())
            {
                return "Please enter a username";
            }

            if ((_authRepo.ValidateEmail(userUpdate.Email) == false) && (userUpdate.Password.Length < 8))
            {
                return "Invalid email and password";
            }

            if (_authRepo.ValidateEmail(userUpdate.Email) == false)
            {
                return "Invalid email";
            }

            if (userUpdate.Password.Length < 8)
            {
                return "Password must be at least 8 characters";
            }

            _authRepo.CreatePasswordHash(userUpdate.Password, out byte[] passwordHash, out byte[] passwordSalt);

            var user = new User
            {
                Id = userId,
                Username = userUpdate.Username,
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt,
                Email = userUpdate.Email,
            };

            await _userData.UpdateUser(user);

            return "Updated Successfully!";
        }
    }
}
