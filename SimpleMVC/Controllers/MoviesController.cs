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
            var data = await context.Movies.Include(m => m.Cinema).ToListAsync();
            return View(data);
        }
    }
}
