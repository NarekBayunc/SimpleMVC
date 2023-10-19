using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SimpleMVC.Data.CustomAttributes;
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

        [Authorize]
        public async Task<IActionResult> LogOut()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            TempData["LoggedOut"] = "You have successfully Logged Out";
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
            User? currentUser = await service.GetUser(user);
            if (currentUser == null || !ModelState.IsValid)
            {
                TempData["ErrorMessage"] = "Invalid data input, please try again";
                return View(user);
            }
            else
            {
                await LogInViaCookie(currentUser);
                return RedirectToAction("Index", "Movies", TempData["LoggedIn"]="True");
            }
        }
        [AllowAnonymousOnly]
        public IActionResult Register()
        {
            return View();
        }
        [AllowAnonymousOnly]
        [HttpPost]
        public async Task<IActionResult> Register([Bind("Name,Email,Password")]User user)
        {
            if (user == null || !ModelState.IsValid)
            {
                TempData["ErrorMessage"] = "Invalid data input, please try again";
                return View(user);
            }
            else
            {
                await service.AddAsync(user);
                await LogInViaCookie(user);
            }
            return RedirectToAction("Index", "Movies", TempData["SignedIn"] = "True");
        }

        public async Task LogInViaCookie(User user)
        {
            string authScheme = CookieAuthenticationDefaults.AuthenticationScheme;
            List<Claim>? claims = new List<Claim>()
                            { new Claim(ClaimTypes.Name, user.Name!),
                              new Claim(ClaimTypes.Email, user.Email!)
                            };
            ClaimsIdentity? identity = new ClaimsIdentity(claims, authScheme);
            AuthenticationProperties authProp = new AuthenticationProperties()
            {
                AllowRefresh = true,
                IsPersistent = false
            };
            await HttpContext.SignInAsync(authScheme, new ClaimsPrincipal(identity), authProp);
        }


    }
}
