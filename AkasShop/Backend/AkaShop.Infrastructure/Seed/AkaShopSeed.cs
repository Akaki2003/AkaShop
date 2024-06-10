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
                     new (){ Email = "Admin@gmail.com", UserName="Admin",PasswordHash="Admin123!",EmailConfirmed = true },
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
            var products = new List<Product>
            {
                new () { Name = "Fancy Hat", Price = 1500, Description = "A stylish hat for any occasion.", ImageUrl = "https://images.pexels.com/photos/324730/pexels-photo-324730.jpeg?auto=compress&cs=tinysrgb&dpr=1&w=500", UserId = 1},
                new () { Name = "Vintage Camera", Price = 2500, Description = "Capture memories with this classic camera.", ImageUrl = "https://images.pexels.com/photos/408508/pexels-photo-408508.jpeg?auto=compress&cs=tinysrgb&dpr=1&w=500", UserId = 1},
                new () { Name = "Artisanal Coffee Maker", Price = 3500, Description = "Brew the perfect cup with this handcrafted coffee maker.", ImageUrl = "https://images.pexels.com/photos/209151/pexels-photo-209151.jpeg?auto=compress&cs=tinysrgb&dpr=1&w=500", UserId = 1},
                new () { Name = "Designer Sneakers", Price = 4500, Description = "Step out in style with these trendy sneakers.", ImageUrl = "https://images.pexels.com/photos/2845548/pexels-photo-2845548.jpeg?auto=compress&cs=tinysrgb&dpr=1&w=500", UserId = 1},
                new () { Name = "Handmade Jewelry Set", Price = 5500, Description = "Elevate your look with this exquisite jewelry set.", ImageUrl = "https://images.pexels.com/photos/106156/pexels-photo-106156.jpeg?auto=compress&cs=tinysrgb&dpr=1&w=500", UserId = 1},
            };

            if (!context.Products.Any())
            {
                context.Products.AddRange(products);
                seeded = true;
            }
        }

    }
}
