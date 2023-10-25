using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SimpleMVC.Data.Services;
using SimpleMVC.Models;
using System.Security.Claims;
using SimpleMVC.Data;
using SimpleMVC.Data.Enums;

namespace SimpleMVC.Controllers
{
    [Authorize]
    public class UsersController : Controller
    {
        private readonly UserService service;
        public UsersController(UserService service)
        {
            this.service = service;
        }
        public async Task<IActionResult> Index()
        {
            string? userEmail = User.FindFirstValue(ClaimTypes.Email);
            if (userEmail != null)
            {
                User? user = await service.GetByEmailAsync(userEmail);
                return View(user);
            }
         
            return View();
        }
        public async Task<IActionResult> Edit(int id)
        {
            User? user = await service.GetByIdAsync(id);
            if (user != null)
            {
                return View(user);
            }
            return RedirectToAction(nameof(Index));
        }
        [HttpPost]
        public async Task<IActionResult> Edit(User user, IFormFile? pictureData,
                                              string pictureDataString)
        {
            ModelState.Remove("Password");
            byte[]? imageData = Helper.FromImgToBytes(pictureData, pictureDataString);
            user.PictureData = imageData;
            if (!ModelState.IsValid || user == null)
            {
                TempData["ErrorMessage"] = "Invalid data input, please try again";
                return View(user);
            }
            else
            {
                bool updateResult = await service.UpdateAsync(user.Id, user);
                if (updateResult)
                {
                    await RefreshSignInAsync(user);
                    return RedirectToAction("Index", "Movies");
                }
                return View(user);
            }

        }

        public async Task RefreshSignInAsync(User user)
        {
            string authScheme = CookieAuthenticationDefaults.AuthenticationScheme;
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.Login!),
                new Claim(ClaimTypes.Email, user.Email!),
                new Claim(ClaimsIdentity.DefaultRoleClaimType, user.Role.ToString())
            };

            var claimsIdentity = new ClaimsIdentity(claims, authScheme);

            var authProperties = new AuthenticationProperties
            {
                AllowRefresh = true,
                IsPersistent = false
            };
            await HttpContext.SignOutAsync("Cookies");
            await HttpContext.SignInAsync("Cookies", new ClaimsPrincipal(claimsIdentity), authProperties);
        }
    }
}
