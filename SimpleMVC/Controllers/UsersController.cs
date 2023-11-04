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
            User? userAuthed = null;
            string? userEmail = User.FindFirstValue(ClaimTypes.Email);
            if (userEmail != null && user != null)
            {
                userAuthed = await service.GetByEmailAsync(userEmail);
                if(user?.Id == userAuthed?.Id)
                {
                    return View(user);
                }
            }
            return RedirectToAction("Index","Movies");
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
            else if (user.Email != User.FindFirstValue(ClaimTypes.Email) && !service.IsValidMail(user))
            {
                TempData["ErrorMessage"] = "This Email Already Exists, try another";
                return View(user);
            }
            else
            {
                bool updateResult = await service.UpdateAsync(user.Id, user);
                if (updateResult)
                {
                    User? updatedUser = await service.GetByIdAsync(user.Id);
                    await RefreshSignInAsync(updatedUser);
                    TempData["ProfileEdited"] = "Your data has been successfully changed";
                    return RedirectToAction("Index", "Movies");
                }
                return View(user);
            }

        }

        public async Task RefreshSignInAsync(User user)
        {
            string authScheme;
            ClaimsIdentity? identity;
            AuthenticationProperties authProp;
            Helper.UserCookieConfig(user, out authScheme, out identity, out authProp);

            await HttpContext.SignOutAsync(authScheme);
            await HttpContext.SignInAsync(authScheme, new ClaimsPrincipal(identity!), authProp);
        }
    }
}
