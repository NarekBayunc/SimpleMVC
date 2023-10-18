using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SimpleMVC.Data.Services;
using SimpleMVC.Models;

namespace SimpleMVC.Controllers
{
    [Authorize]
    public class ProducersController : Controller
    {
        private readonly IEntityControllerService<Producer> service;
        public ProducersController(IEntityControllerService<Producer> service)
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
        public async Task<IActionResult> Create([Bind("FullName, ProfilePictureURL, Bio")] Producer producer)
        {
            if (!ModelState.IsValid)
            {
                return View(producer);
            }
            else
            {
                await service.AddAsync(producer);
                return RedirectToAction(nameof(Index));
            }
        }
        public async Task<IActionResult> Details(int id)
        {
            Producer? producer = await service.GetByIdAsync(id);
            RedirectIfNull(producer);
            return View(producer);
        }

        public async Task<IActionResult> Edit(int id)
        {
            Producer? producer = await service.GetByIdAsync(id);
            RedirectIfNull(producer);
            return View(producer);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(Producer producer)
        {
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

        public async Task<IActionResult> Delete(int id)
        {
            Producer? producer = await service.GetByIdAsync(id);
            RedirectIfNull(producer);
            return View(producer);
        }
        [HttpPost]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await RedirectIfNullAsync(id);
            await service.RemoveAsync(id);
            return RedirectToAction(nameof(Index));
        }

        public RedirectResult RedirectIfNull(Producer? producer)
        {
            if (producer == null) return Redirect("/Producers/Index");
            else return null!;
        }
        public async Task<RedirectResult> RedirectIfNullAsync(int id)
        {
            Producer? producer = await service.GetByIdAsync(id);
            if (producer == null) return Redirect("/Producers/Index");
            else return null!;
        }
    }
}
