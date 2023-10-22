using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using SimpleMVC.Data;
using SimpleMVC.Data.Services;
using SimpleMVC.Models;

namespace SimpleMVC.Controllers
{
    [Authorize]
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
        public async Task<IActionResult> Create([Bind("Name, Description, LogoData")] Cinema cinema,
                                                IFormFile? logoData)
        {
            if (logoData == null && !(logoData?.Length > 0))
            {
                cinema.LogoData = Helper.DefaultImageForLogo();
            }
            else
            {
                cinema.LogoData = Helper.FromImgToBytes(logoData);
            }
            if (ModelState.IsValid)
            {
                await service.AddAsync(cinema);
                return RedirectToAction(nameof(Index));
            }
            return View(cinema);
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
