
var builder = WebApplication.CreateBuilder(args);
var services = builder.Services;
var connStr = builder.Configuration.GetConnectionString("DefaultConnection");

var hcBuilder = services.AddHealthChecks();

hcBuilder
   .AddNpgSql(connStr, name: "dbHealthCheck");


// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddHealthChecksUI().AddInMemoryStorage();

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

app
.UseRouting()
.UseEndpoints(endpoints => endpoints
.MapHealthChecksUI(healthChecks => healthChecks.UIPath = "/healthchecks"));


app.UseAuthorization();


app.MapRazorPages();

app.Run();
