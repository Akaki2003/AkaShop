using AkaShop.Application.Users.Repositories;
using AkaShop.Application.Users.Requests;
using AkaShop.Application.Users.Responses;
using AkaShop.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;

namespace AkaShop.Application.Users
{
    public class UserService( IConfiguration config, IUserRepository userRepository) : IUserService
    {
        private readonly IConfiguration _config = config;
        private readonly IUserRepository _userRepo = userRepository;

        public async Task<ProfileResponseModel> GetProfile(int? id) => await _userRepo.GetProfile(id);
        public async Task<ProfileResponseModel> Login(UserModel model) => await _userRepo.Login(model);
        public async Task<ProfileResponseModel> CreateUserAsync(UserModel model) =>     await _userRepo.CreateUserAsync(model);
    }
}
