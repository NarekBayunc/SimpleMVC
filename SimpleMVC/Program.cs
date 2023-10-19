using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SimpleMVC.Data;
using SimpleMVC.Data.Services;
using SimpleMVC.Models;

var builder = WebApplication.CreateBuilder(args);

// PRODUCERS EDIT BUG NAME SAVED WITHOUT DATA

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


void GetServices()
{
    string? connection = builder.Configuration.GetConnectionString("DefaultConnection");
    builder.Services.AddControllersWithViews();
    builder.Services.AddDbContext<ApplicationContext>(options => options.UseSqlServer(connection));
    builder.Services.AddScoped<IEntityControllerService<Actor>, BaseEntityService<Actor>>();
    builder.Services.AddScoped<IEntityControllerService<Cinema>, BaseEntityService<Cinema>>(); 
    builder.Services.AddScoped<IEntityControllerService<Producer>, BaseEntityService<Producer>>();
    builder.Services.AddScoped<IEntityControllerService<Movie>, BaseEntityService<Movie>>();
    builder.Services.AddScoped<UserService>();
    builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                                        .AddCookie(
                                        option =>
                                        {
                                            option.LoginPath = "/Auth/Login";
                                            option.ExpireTimeSpan = TimeSpan.FromMinutes(5);
                                            option.AccessDeniedPath = "/Auth/Login";
                                        });
}