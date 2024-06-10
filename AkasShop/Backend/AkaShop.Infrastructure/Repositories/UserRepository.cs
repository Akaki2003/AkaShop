using AkaShop.Application.Users.Repositories;
using AkaShop.Application.Users.Requests;
using AkaShop.Application.Users.Responses;
using AkaShop.Domain.Entities;
using AkaShop.Infrastructure.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace AkaShop.Infrastructure.Repositories
{
    public class UserRepository(ApplicationDbContext context, UserManager<User> userManager) : BaseRepository<User>(context), IUserRepository
    {
        private readonly UserManager<User> _userManager = userManager;

        public async Task<ProfileResponseModel> GetProfile(int? id)
        {
            var profile = await _dbSet.Where(x => x.Id == id).Select(user =>
                new ProfileResponseModel()
                {
                    Email = user.Email,
                    Id = user.Id,
                    ProductCount = user.Products.Count
                }).FirstOrDefaultAsync();

            if (profile != null)
                return profile;
            return null;
        }
        public async Task<ProfileResponseModel> Login(UserModel model)
        {
            var user = await _userManager.FindByNameAsync(model.Email);
            user ??= await _userManager.FindByEmailAsync(model.Email);
            if (await _userManager.CheckPasswordAsync(user, model.Password))
            {
                return new ProfileResponseModel
                {
                    Email = user.Email,
                    Id = user.Id,
                };
            }
            return null;
        }
        public async Task<ProfileResponseModel> CreateUserAsync(UserModel model)
        {
            var user = new User
            {
                UserName = model.Email,
                Email = model.Email
            };
            var result = await _userManager.CreateAsync(user, model.Password);
            if (result.Succeeded)
            {
                return new ProfileResponseModel
                {
                    Email = user.Email,
                    Id = user.Id,
                };
            }
            return null;
        }
    }
}
