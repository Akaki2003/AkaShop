using AkaShop.Domain.Entities;
using IdentityServer4.EntityFramework.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace AkaShop.Infrastructure.Data
{
    public class ApplicationDbContext : IdentityDbContext <User,UserRole,int>
    {
        private readonly IConfiguration _configuration;
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options, IConfiguration configuration) : base(options)
        {
            _configuration = configuration;
        }

        //DbSets
        public new DbSet<User> Users { get; set; }
        public DbSet<Product> Products { get; set; }

        //Configurations
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
            builder.Entity<User>().ToTable("Users");
            builder.Entity<UserRole>().ToTable("Roles");
        }

    }
}
