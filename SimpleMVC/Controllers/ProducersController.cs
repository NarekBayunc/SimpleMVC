using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SimpleMVC.Data;
using SimpleMVC.Data.CustomAttributes;
using SimpleMVC.Data.Services;
using SimpleMVC.Models;

namespace SimpleMVC.Controllers
{
    [Authorize]
    public class ProducersController : Controller
    {
        private const string _indexPage = "/Producers/Index";
        private readonly IEntityControllerService<Producer> service;
        public ProducersController(IEntityControllerService<Producer> service)
        {
            this.service = service;
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
                await service.AddAsync(producer);
                return RedirectToAction(nameof(Index));
            }
            return View(producer);
        }
        [RedirectIfNull(_indexPage)]
        public async Task<IActionResult> Details(int id)
        {
            Producer? producer = await service.GetByIdAsync(id);
            if (producer == null)
            {
                return RedirectToAction(nameof(Index));
            }
            return View(producer);
        }
        [RedirectIfNull(_indexPage)]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int id)
        {
            Producer? producer = await service.GetByIdAsync(id);
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
                return RedirectToAction(nameof(Index));
            }
        }
        [RedirectIfNull(_indexPage)]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int id)
        {
            Producer? producer = await service.GetByIdAsync(id);
            if (producer == null)
            {
                return RedirectToAction(nameof(Index));
            }
            return View(producer);
        }
        [HttpPost]
        [RedirectIfNull(_indexPage)]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await service.RemoveAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
