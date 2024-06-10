using AkaShop.Application.Products.Repositories;
using AkaShop.Application.Products;
using AkaShop.Application.Users.Repositories;
using AkaShop.Application.Users;
using Microsoft.EntityFrameworkCore;
using AkaShop.Infrastructure.Data;
using AkaShop.Infrastructure.Repositories;

namespace AkaShop.Infrastructure.Extensions
{
    public static class ServiceExtensions
    {
        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            services.AddScoped<DbContext, ApplicationDbContext>();

            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<IUserService, UserService>();


            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<IUserRepository, UserRepository>();

            return services;
        }
    }
}
