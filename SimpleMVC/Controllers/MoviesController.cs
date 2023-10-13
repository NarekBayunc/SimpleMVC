using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SimpleMVC.Data;

namespace SimpleMVC.Controllers
{
    public class MoviesController : Controller
    {
        private readonly ApplicationContext context;
        public MoviesController(ApplicationContext context)
        {
            this.context = context;
        }
        public async Task<IActionResult> Index()
        {
            var data = await context.Movies.ToListAsync();
            return View(data);
        }
    }
}
