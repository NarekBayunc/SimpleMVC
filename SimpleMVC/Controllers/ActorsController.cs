using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SimpleMVC.Data;
using SimpleMVC.Data.Services;
using SimpleMVC.Models;

namespace SimpleMVC.Controllers
{
    [Authorize]
    public class ActorsController : Controller
    {
        private readonly IEntityControllerService<Actor> service;
        public ActorsController(IEntityControllerService<Actor> service)
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
        public async Task<IActionResult> Create([Bind("FullName, Bio, PictureData")]Actor actor,
                                                            IFormFile? pictureData)
        {
            if (pictureData == null && !(pictureData?.Length > 0))
            {
                actor.PictureData = Helper.DefaultImage();
            }
            else
            {
                actor.PictureData = Helper.FromImgToBytes(pictureData);
            }
            if (ModelState.IsValid)
            {
                await service.AddAsync(actor);
                return RedirectToAction(nameof(Index));
            }
            return View(actor);
        }

        public async Task<IActionResult> Details(int id)
        {
            Actor? actor = await service.GetByIdAsync(id);
            RedirectIfNull(actor);
            return View(actor);
        }

        public async Task<IActionResult> Edit(int id)
        {
            Actor? actor = await service.GetByIdAsync(id);
            RedirectIfNull(actor);
            return View(actor);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(Actor actor, 
                                              IFormFile? pictureData,
                                              string pictureDataString)
        {
            byte[]? imageData = Helper.FromImgToBytes(pictureData, pictureDataString);
            actor.PictureData = imageData;
            if (!ModelState.IsValid)
            {
                return View(actor);
            }
            else
            {
                await service.UpdateAsync(actor);
                return RedirectToAction(nameof(Index));
            }
        }

        public async Task<IActionResult> Delete(int id)
        {
            Actor? actor = await service.GetByIdAsync(id);
            RedirectIfNull(actor);
            return View(actor);
        }
        [HttpPost]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await RedirectIfNullAsync(id);
            await service.RemoveAsync(id);
            return RedirectToAction(nameof(Index));
        }

        public RedirectResult RedirectIfNull(Actor? actor)
        {
            if (actor == null) return Redirect("/Actors/Index");
            else return null!;
        }
        public async Task<RedirectResult> RedirectIfNullAsync(int id)
        {
            Actor? actor = await service.GetByIdAsync(id);
            if (actor == null) return Redirect("/Actors/Index");
            else return null!;
        }
    }
}
