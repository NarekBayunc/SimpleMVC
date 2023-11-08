using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using SimpleMVC.Data;
using SimpleMVC.Data.Services;
using SimpleMVC.Models;

var builder = WebApplication.CreateBuilder(args);
GetServices();
var app = builder.Build();

await AppDbInitializer.Seed(app);

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Movies}/{action=Index}/{id?}");

app.Run();

//TODO CANT CREATE MOVIE
void GetServices()
{
    string? connection = builder.Configuration.GetConnectionString("DefaultConnection");
    builder.Services.AddControllersWithViews();
    builder.Services.AddDbContext<ApplicationContext>(options =>
    {
        options.UseSqlServer(connection);
        options.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
    });
    builder.Services.AddScoped<IEntityControllerService<Actor>, BaseEntityService<Actor>>();
    builder.Services.AddScoped<IEntityControllerService<Cinema>, BaseEntityService<Cinema>>(); 
    builder.Services.AddScoped<IEntityControllerService<Producer>, BaseEntityService<Producer>>();
    builder.Services.AddScoped<IEntityControllerService<Movie>, BaseEntityService<Movie>>();
    builder.Services.AddScoped<UserService>();
    builder.Services.AddScoped<CartService>();
    builder.Services.AddMemoryCache();
    builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                                        .AddCookie(
                                        option =>
                                        {
                                            option.LoginPath = "/Auth/Login";
                                            option.ExpireTimeSpan = TimeSpan.FromMinutes(10);
                                            option.AccessDeniedPath = "/Auth/Login";
                                        });
}