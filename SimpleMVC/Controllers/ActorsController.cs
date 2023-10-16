using Microsoft.AspNetCore.Mvc;
using SimpleMVC.Data.Services;
using SimpleMVC.Models;

namespace SimpleMVC.Controllers
{
    public class ActorsController : Controller
    {
        private readonly IActorService service;
        public ActorsController(IActorService service)
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
        public async Task<IActionResult> Create([Bind("FullName, ProfilePictureURL, Bio")]Actor actor)
        {
            if (!ModelState.IsValid)
            {
                return View(actor);
            }
            else
            {
                await service.AddAsync(actor);
                return RedirectToAction(nameof(Index));
            }
        }

        public async Task<IActionResult> Details(int id)
        {
            Actor? actor = await RedirectIfNotExist(id);
            return View(actor);
        }

        public async Task<IActionResult> Edit(int id)
        {
            Actor? actor = await RedirectIfNotExist(id);
            return View(actor);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(int id, Actor actor)
        {
            if (!ModelState.IsValid)
            {
                return View(actor);
            }
            else
            {
                await service.UpdateAsync(id, actor);
                return RedirectToAction(nameof(Index));
            }
        }

        public async Task<IActionResult> Delete(int id)
        {
            Actor? actor = await RedirectIfNotExist(id);
            return View(actor);
        }
        [HttpPost]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            Actor? actor = await RedirectIfNotExist(id);
            await service.RemoveAsync(id);
            return RedirectToAction(nameof(Index));
        }

        public async Task<Actor?> RedirectIfNotExist(int id)
        {
            Actor? actor = await service.GetByIdAsync(id);
            if (actor == null) Redirect("/Actors/Index");
            return actor;
        }
    }
}
