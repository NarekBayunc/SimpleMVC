using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using SimpleMVC.Data;
using SimpleMVC.Data.CustomAttributes;
using SimpleMVC.Data.Extensions;
using SimpleMVC.Data.Services;
using SimpleMVC.Models;
using SimpleMVC.Models.ViewModels;

namespace SimpleMVC.Controllers
{
    [Authorize]
    public class ProducersController : Controller
    {
        private const string indexPage = "/Producers/Index";
        private readonly IEntityControllerService<Producer> service;
        private IMemoryCache cache;
        private readonly MemoryCacheEntryOptions cacheOptions = new MemoryCacheEntryOptions()
                                                    .SetAbsoluteExpiration(TimeSpan.FromMinutes(30));
        public ProducersController(IEntityControllerService<Producer> service, IMemoryCache cache)
        {
            this.service = service;
            this.cache = cache;
        }
        public async Task<IActionResult> Index(int page = 1)
        {
            IndexViewModel<Producer> ivm = await Helper.GetPaginatedViewModel(page, service);
            return View(ivm);
        }
        [Authorize(Roles = "Admin")]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create([Bind("FullName, Bio, PictureData")] Producer producer,
                                                            IFormFile? pictureData)
        {
            if (pictureData == null && !(pictureData?.Length > 0))
            {
                producer.PictureData = Helper.DefaultImage();
            }
            else
            {
                producer.PictureData = Helper.FromImgToBytes(pictureData);
            }
            if (ModelState.IsValid)
            {
                int producerId = await service.AddAsync(producer);
                cache.Set(producerId, producer, cacheOptions);
                return RedirectToAction(nameof(Index));
            }
            return View(producer);
        }
        [RedirectIfNull(indexPage)]
        public async Task<IActionResult> Details(int id)
        {
            Producer? producer = await this.GetObjectFromDbOrCache(id, service.GetByIdAsync, cache);
            if (producer == null)
            {
                return RedirectToAction(nameof(Index));
            }
            return View(producer);
        }
        [RedirectIfNull(indexPage)]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int id)
        {
            Producer? producer = await this.GetObjectFromDbOrCache(id, service.GetByIdAsync, cache);
            if (producer == null)
            {
                return RedirectToAction(nameof(Index));
            }
            return View(producer);
        }
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(Producer producer, 
                                              IFormFile? pictureData,
                                              string pictureDataString)
        {
            byte[]? imageData = Helper.FromImgToBytes(pictureData, pictureDataString);
            producer.PictureData = imageData;
            if (!ModelState.IsValid)
            {
                return View(producer);
            }
            else
            {
                await service.UpdateAsync(producer);
                cache.Remove(producer);
                cache.Set(producer.Id, producer, cacheOptions);
                return RedirectToAction(nameof(Index));
            }
        }
        [RedirectIfNull(indexPage)]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int id)
        {
            Producer? producer = await this.GetObjectFromDbOrCache(id, service.GetByIdAsync, cache);
            if (producer == null)
            {
                return RedirectToAction(nameof(Index));
            }
            return View(producer);
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
