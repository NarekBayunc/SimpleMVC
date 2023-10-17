using Microsoft.AspNetCore.Mvc;
using SimpleMVC.Data.Services;
using SimpleMVC.Models;

namespace SimpleMVC.Controllers
{
    public class CinemasController : Controller
    {
        private readonly IEntityControllerService<Cinema> service;
        public CinemasController(IEntityControllerService<Cinema> service)
        {
            this.service = service;
        }
        public async Task<IActionResult> Index()
        {
            return View(await service.GetAllAsync());
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create([Bind("Name, Logo, Description")] Cinema cinema)
        {
            if (!ModelState.IsValid)
            {
                return View(cinema);
            }
            else
            {
                await service.AddAsync(cinema);
                return RedirectToAction(nameof(Index));
            }
        }

        public async Task<IActionResult> Details(int id)
        {
            Cinema? cinema = await service.GetByIdAsync(id);
            RedirectIfNull(cinema);
            return View(cinema);
        }

        public async Task<IActionResult> Edit(int id)
        {
            Cinema? cinema = await service.GetByIdAsync(id);
            RedirectIfNull(cinema);
            return View(cinema);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(Cinema cinema)
        {
            if (!ModelState.IsValid)
            {
                return View(cinema);
            }
            else
            {
                await service.UpdateAsync(cinema);
                return RedirectToAction(nameof(Index));
            }
        }

        public async Task<IActionResult> Delete(int id)
        {
            Cinema? cinema = await service.GetByIdAsync(id);
            RedirectIfNull(cinema);
            return View(cinema);
        }
        [HttpPost]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await RedirectIfNullAsync(id);
            await service.RemoveAsync(id);
            return RedirectToAction(nameof(Index));
        }

        public RedirectResult RedirectIfNull(Cinema? cinema)
        {
            if (cinema == null) return Redirect("/Cinemas/Index");
            else return null!;
        }
        public async Task<RedirectResult> RedirectIfNullAsync(int id)
        {
            Cinema? cinema = await service.GetByIdAsync(id);
            if (cinema == null) return Redirect("/Cinemas/Index");
            else return null!;
        }
    }
}
