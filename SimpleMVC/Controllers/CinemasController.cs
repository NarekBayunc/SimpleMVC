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
    [Authorize]
    public class CinemasController : Controller
    {
        private const string indexPage = "/Cinemas/Index";
        private readonly IEntityControllerService<Cinema> service;
        private IMemoryCache cache;
        private readonly MemoryCacheEntryOptions cacheOptions = new MemoryCacheEntryOptions()
                                                    .SetAbsoluteExpiration(TimeSpan.FromMinutes(30));
        public CinemasController(IEntityControllerService<Cinema> service, IMemoryCache cache)
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
        public async Task<IActionResult> Create([Bind("Name, Description, LogoData")] Cinema cinema,
                                                IFormFile? logoData)
        {
            if (logoData == null && !(logoData?.Length > 0))
            {
                cinema.LogoData = Helper.DefaultImage();
            }
            else
            {
                cinema.LogoData = Helper.FromImgToBytes(logoData);
            }
            if (ModelState.IsValid)
            {
                int cinemaId = await service.AddAsync(cinema);
                cache.Set(cinemaId, cinema, cacheOptions);
                return RedirectToAction(nameof(Index));
            }
            return View(cinema);
        }
        [RedirectIfNull(indexPage)]
        public async Task<IActionResult> Details(int id)
        {
            Cinema? cinema = await this.GetObjectFromDbOrCache(id, service.GetByIdAsync, cache);
            if (cinema == null)
            {
                return RedirectToAction(nameof(Index));
            }
            return View(cinema);
        }
        [RedirectIfNull(indexPage)]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int id)
        {
            Cinema? cinema = await this.GetObjectFromDbOrCache(id, service.GetByIdAsync, cache);
            if (cinema == null)
            {
                return RedirectToAction(nameof(Index));
            }
            return View(cinema);
        }
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(Cinema cinema,
                                              IFormFile? logoData,
                                              string logoDataString)
        {
            byte[]? imageData = Helper.FromImgToBytes(logoData, logoDataString);
            cinema.LogoData = imageData;
            if (!ModelState.IsValid)
            {
                return View(cinema);
            }
            else
            {
                await service.UpdateAsync(cinema);
                cache.Remove(cinema);
                cache.Set(cinema.Id, cinema, cacheOptions);
                return RedirectToAction(nameof(Index));
            }
        }
        [RedirectIfNull(indexPage)]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int id)
        {
            Cinema? cinema = await this.GetObjectFromDbOrCache(id, service.GetByIdAsync, cache);
            if (cinema == null)
            {
                return RedirectToAction(nameof(Index));
            }
            return View(cinema);
        }
        [HttpPost]
        [RedirectIfNull(indexPage)]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            cache.Remove(id);
            await service.RemoveAsync(id);
            return RedirectToAction(nameof(Index));
        }

    }
}
