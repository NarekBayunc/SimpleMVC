using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SimpleMVC.Data;

namespace SimpleMVC.Controllers
{
    public class ProducersController : Controller
    {
        private readonly ApplicationContext context;
        public ProducersController(ApplicationContext context)
        {
            this.context = context;
        }
        public async Task<IActionResult> Index()
        {
            var data = await context.Producers.ToListAsync();
            return View(data);
        }
    }
}
