using FictionAPI.DTOs;
using FictionAPI.Interfaces;
using FictionDataAccessLibrary.Data;
using FictionDataAccessLibrary.Models;
using Microsoft.AspNetCore.SignalR;

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

        public async Task UpdateUser(UpdateUserDto userUpdate, int userId)
        {
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
        }
    }
}
