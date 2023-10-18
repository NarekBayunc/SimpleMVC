using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using SimpleMVC.Data.Services;
using SimpleMVC.Models;
using System.Security.Claims;

namespace SimpleMVC.Controllers
{

    public class AuthController : Controller
    {
        private readonly UserService service;
        public AuthController(UserService service)
        {
            this.service = service;
        }

        public async Task<IActionResult> LogOut()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return Redirect("/Auth/Login");
        }
        public IActionResult Login()
        {
            ClaimsPrincipal claimUser = HttpContext.User;

            if (claimUser.Identity != null && claimUser.Identity.IsAuthenticated)
            {
                return Redirect("/Movies/Index");
            }

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login([Bind("Name,Email,Password")]User user)
        { 
            User? currentUser = service.GetUser(user);
            if (currentUser == null || !ModelState.IsValid)
            {
                TempData["ErrorMessage"] = "Invalid data input, please try again";
                return View(user);
            }
            else
            {
                string authScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                List<Claim>? claims = new List<Claim>()
                            { new Claim(ClaimTypes.Name, currentUser.Name!),
                              new Claim(ClaimTypes.Email, currentUser.Email!)
                            };
                ClaimsIdentity? identity = new ClaimsIdentity(claims, authScheme);
                AuthenticationProperties authProp = new AuthenticationProperties()
                {
                    AllowRefresh = true,
                    IsPersistent = false
                };
                await HttpContext.SignInAsync(authScheme, new ClaimsPrincipal(identity), authProp);
                return RedirectToAction("Index", "Movies", TempData["LoggedIn"]="True");
            }
        }
    }
}
