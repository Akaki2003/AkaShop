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
                new () { Name = "Fancy Hat", Price = 15, Description = "A stylish hat for any occasion.", ImageUrl = "https://images.pexels.com/photos/7084527/pexels-photo-7084527.jpeg?auto=compress&cs=tinysrgb&w=600", UserId = 1},
                new () { Name = "Vintage Camera", Price = 25, Description = "Capture memories with this classic camera.", ImageUrl = "https://images.pexels.com/photos/37397/camera-old-antique-voigtlander.jpg?auto=compress&cs=tinysrgb&w=1260&h=750&dpr=1", UserId = 1},
                new () { Name = "Artisanal Coffee Maker", Price = 35, Description = "Brew the perfect cup with this handcrafted coffee maker.", ImageUrl = "https://images.pexels.com/photos/4993062/pexels-photo-4993062.jpeg?auto=compress&cs=tinysrgb&w=1260&h=750&dpr=1", UserId = 1},
                new () { Name = "Designer Sneakers", Price = 45, Description = "Step out in style with these trendy sneakers.", ImageUrl = "https://images.pexels.com/photos/1750045/pexels-photo-1750045.jpeg?auto=compress&cs=tinysrgb&w=1260&h=750&dpr=1", UserId = 1},
                new () { Name = "Handmade Jewelry Set", Price = 55, Description = "Elevate your look with this exquisite jewelry set.", ImageUrl = "https://images.pexels.com/photos/1395306/pexels-photo-1395306.jpeg?auto=compress&cs=tinysrgb&w=1260&h=750&dpr=1", UserId = 1},
            };

            if (!context.Products.Any())
            {
                context.Products.AddRange(products);
                seeded = true;
            }
        }

    }
}
