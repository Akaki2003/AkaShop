using AkaShop.Auth;
using AkaShop.Domain.Entities;
using AkaShop.Infrastructure.Data;
using AkaShop.Infrastructure.Extensions;
using AkaShop.Infrastructure.Seed;
using Microsoft.EntityFrameworkCore;
using Swashbuckle.AspNetCore.Filters;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);
var services = builder.Services;
var configuration = builder.Configuration;
var env = builder.Environment;
var migrationsAssembly = "AkaShop.Infrastructure";
string connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

// Add services to the container.
services.AddMemoryCache();

services.AddAkaShopDb(configuration);

builder.Services.AddDefaultIdentity<User>(options =>
{
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequiredLength = 6;
})
    .AddRoles<UserRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>();

services.AddCors();
services.AddControllers();

services.Configure<JWTConfiguration>(builder.Configuration.GetSection(nameof(JWTConfiguration)));

services.AddEndpointsApiExplorer();
services.AddMyAuthentication(configuration);

services.AddServices();

services.AddSwaggerDocumentation(configuration);

builder.Services.AddSwaggerExamplesFromAssemblies(Assembly.GetExecutingAssembly());


var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors(builder =>
{
    var addresses = new string[] { "http://localhost:5173", "https://localhost:7234/" };
    builder
    .WithOrigins(addresses)
    .AllowAnyMethod().AllowCredentials().AllowAnyHeader();
});

app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();


app.MapControllers();
app.UseEndpoints(endpoints =>
{
    _ = endpoints.MapDefaultControllerRoute();
});

await AkaShopSeed.Initialize(app.Services);

app.Run();


