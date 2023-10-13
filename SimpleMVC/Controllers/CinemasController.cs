using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SimpleMVC.Data;

namespace SimpleMVC.Controllers
{
    public class CinemasController : Controller
    {
        private readonly ApplicationContext context;
        public CinemasController(ApplicationContext context)
        {
            this.context = context;
        }
        public async Task<IActionResult> Index()
        {
            var data = await context.Cinemas.ToListAsync();
            return View(data);
        }
    }
}
