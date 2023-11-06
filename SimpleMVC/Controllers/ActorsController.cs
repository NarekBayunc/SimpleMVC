using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using SimpleMVC.Data;
using SimpleMVC.Data.CustomAttributes;
using SimpleMVC.Data.Extensions;
using SimpleMVC.Data.Services;
using SimpleMVC.Models;

namespace SimpleMVC.Controllers
{
    public class ActorsController : Controller
    {
        private const string indexPage = "/Actors/Index";
        private readonly IEntityControllerService<Actor> service;
        private IMemoryCache cache;
        private readonly MemoryCacheEntryOptions cacheOptions = new MemoryCacheEntryOptions()
                                                    .SetAbsoluteExpiration(TimeSpan.FromMinutes(30));
        public ActorsController(IEntityControllerService<Actor> service, IMemoryCache cache)
        {
            this.service = service;
            this.cache = cache;
        }
        public async Task<IActionResult> Index()
        {
            return View(await service.GetAllAsync());
        }
        [Authorize(Roles = "Admin")]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [Authorize(Roles = "Admin")]
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
                int actorId = await service.AddAsync(actor);
                cache.Set(actorId, actor, cacheOptions);
                return RedirectToAction(nameof(Index));
            }
            return View(actor);
        }
        [RedirectIfNull(indexPage)]
        public async Task<IActionResult> Details(int id)
        {
            Actor? actor = await this.GetObjectFromDbOrCache(id, service.GetByIdAsync, cache);
            if (actor == null)
            {
                return RedirectToAction(nameof(Index));
            }
            return View(actor);
        }
        [Authorize(Roles = "Admin")]
        [RedirectIfNull(indexPage)]
        public async Task<IActionResult> Edit(int id)
        {
            Actor? actor = await this.GetObjectFromDbOrCache(id, service.GetByIdAsync, cache);
            if (actor == null)
            {
                return RedirectToAction(nameof(Index));
            }
            return View(actor);
        }
        [Authorize(Roles = "Admin")]
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
                cache.Remove(actor);
                cache.Set(actor.Id, actor, cacheOptions);
                return RedirectToAction(nameof(Index));
            }
        }
        [Authorize(Roles = "Admin")]
        [RedirectIfNull(indexPage)]
        public async Task<IActionResult> Delete(int id)
        {
            Actor? actor = await this.GetObjectFromDbOrCache(id, service.GetByIdAsync, cache);
            if (actor == null)
            {
                return RedirectToAction(nameof(Index));
            }
            return View(actor);
        }
        [HttpPost]
        [Authorize(Roles = "Admin")]
        [RedirectIfNull(indexPage)]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            cache.Remove(id);
            await service.RemoveAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
