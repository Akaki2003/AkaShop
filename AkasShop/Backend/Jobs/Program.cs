using AkaShop.Application.Products.Repositories;
using AkaShop.Domain.Entities;
using AkaShop.Infrastructure.Data;
using Hangfire;
using Hangfire.PostgreSql;
using Jobs;
using Jobs.Extensions;
using Microsoft.EntityFrameworkCore;
using Npgsql.EntityFrameworkCore.PostgreSQL.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();

var connString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<ApplicationDbContext>((DbContextOptionsBuilder opt) =>
{
    opt.UseNpgsql(connString, (NpgsqlDbContextOptionsBuilder a) =>
    {
        a.MigrationsAssembly("AkaShop.Infrastructure");
    });
});

builder.Services.AddEntityFrameworkNpgsql().AddDbContext<ApplicationDbContext>(options =>
{
    options.UseNpgsql(builder.Configuration.GetConnectionString("defaultConnection"));
});

builder.Services.AddServices();

builder.Services.AddHangfire(x => x.UsePostgreSqlStorage(connString));

builder.Services.AddHangfireServer();

builder.Services.AddDefaultIdentity<User>()
    .AddRoles<UserRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}


app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();

app.UseHangfireDashboard("/dashboard", new DashboardOptions
{
    Authorization = new[] { new JobsAuthorizationFilter() }
});

//RecurringJob.AddOrUpdate<IProductRepository>("RemoveOldProducts", x => x.RemoveOldProducts(), "0 */12 * * *");
RecurringJob.AddOrUpdate<IProductRepository>("RemoveOldProducts", x => x.RemoveOldProducts(), "* * * * *");


app.Run();

