using Microsoft.EntityFrameworkCore;
using SimpleMVC.Data;
using SimpleMVC.Data.Services;
using SimpleMVC.Models;

var builder = WebApplication.CreateBuilder(args);

GetServices();

var app = builder.Build();

await AppDbInitializer.Seed(app);

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

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
    builder.Services.AddScoped<IPersonService<Actor>, ActorsService>();
    builder.Services.AddScoped<IPersonService<Producer>, ProducersService>();
}