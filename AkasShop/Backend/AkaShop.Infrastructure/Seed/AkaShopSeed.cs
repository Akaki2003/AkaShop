using AkaShop.Domain.Entities;
using AkaShop.Infrastructure.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace AkaShop.Infrastructure.Seed
{
    public static class AkaShopSeed
    {
        public async static Task Initialize(IServiceProvider serviceProvider)
        {
            using var scope = serviceProvider.CreateScope();
            var database = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
            var userManager = scope.ServiceProvider.GetRequiredService<UserManager<User>>();

            Migrate(database);
            await SeedEverything(database, userManager);
        }

        private static void Migrate(ApplicationDbContext context)
        {
            context.Database.Migrate();
        }

        private async static Task SeedEverything(ApplicationDbContext context, UserManager<User> userManager)
        {
            var seeded = false;

            await SeedUsers(context, userManager);
            SeedProducts(context, ref seeded);
            if (seeded)
                context.SaveChanges();
        }

        private async static Task SeedUsers(ApplicationDbContext context, UserManager<User> userManager)
        {
            var users = new List<User>
                {
                     new (){ Email = "Admin@gmail.com", UserName="Admin",PasswordHash="Admin123",EmailConfirmed = true },
                };

            if (!context.Users.Any())
            {
                foreach (var user in users)
                {
                    var password = user.PasswordHash;
                    user.PasswordHash = null;
                    var result = await userManager.CreateAsync(user, password);
                }
            }
        }

        public static void SeedProducts(ApplicationDbContext context, ref bool seeded)
        {
            var img = "https://plus.unsplash.com/premium_photo-1680112806039-244731d88d45?q=80&w=1925&auto=format&fit=crop&ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8fA%3D%3D";
            var products = new List<Product>
            {
                new () { Name = "Product 1", Price = 1000, Description = "Description 1", ImageUrl = img, UserId = 1},
                new () { Name = "Product 2", Price = 2000, Description = "Description 2", ImageUrl = img, UserId = 1},
                new () { Name = "Product 3", Price = 3000, Description = "Description 3", ImageUrl = img, UserId = 1},
                new () { Name = "Product 4", Price = 4000, Description = "Description 4", ImageUrl = img, UserId = 1},
                new () { Name = "Product 5", Price = 5000, Description = "Description 5", ImageUrl = img, UserId = 1},
            };

            if (!context.Products.Any())
            {
                context.Products.AddRange(products);
                seeded = true;
            }
        }

    }
}
