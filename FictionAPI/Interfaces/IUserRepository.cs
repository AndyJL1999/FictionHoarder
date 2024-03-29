﻿using FictionAPI.DTOs;
using FictionDataAccessLibrary.Models;

namespace FictionAPI.Interfaces
{
    public interface IUserRepository
    {
        Task<string> UpdateUser(UpdateUserDto userUpdate, int userId);
        Task<User> GetUserByIdAsync(int id);
    }
}