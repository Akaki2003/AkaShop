﻿using AkaShop.Application.Users.Requests;
using AkaShop.Application.Users.Responses;

namespace AkaShop.Application.Users.Repositories
{
    public interface IUserRepository
    {
        Task<ProfileResponseModel> CreateUserAsync(UserModel model);
        Task<ProfileResponseModel> GetProfile(int? id);
        Task<ProfileResponseModel> Login(UserModel model);
    }
}
