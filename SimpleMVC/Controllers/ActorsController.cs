using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SimpleMVC.Data;

namespace SimpleMVC.Controllers
{
    public class ActorsController : Controller
    {
        private readonly ApplicationContext context;
        public ActorsController(ApplicationContext context)
        {
            this.context = context;
        }
        public async Task<IActionResult> Index()
        {
            return View(await context.Actors.ToListAsync());
        }
    }
}
