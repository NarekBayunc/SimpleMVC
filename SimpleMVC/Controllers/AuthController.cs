﻿using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SimpleMVC.Data;
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
                return RedirectToAction("Index", "Movies");
            }

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(User user)
        { 
            User? currentUser = await service.GetUser(user);
            if (currentUser == null)
            {
                var errors = ModelState.Values.SelectMany(v => v.Errors);
                TempData["ErrorMessage"] = "Invalid data input, please try again";
                return View(user);
            }
            else
            {
                await LogInViaCookie(currentUser);
                return RedirectToAction("Index", "Movies", TempData["LoggedIn"]=currentUser.Name);
            }
        }
        [AllowAnonymousOnly]
        public IActionResult Register()
        {
            return View();
        }
        [AllowAnonymousOnly]
        [HttpPost]
        public async Task<IActionResult> Register([Bind("Name,Age,Login,Email,Password")]User user)
        {
            if (user == null || !ModelState.IsValid)
            {
                TempData["ErrorMessage"] = "Invalid data input, please try again";
                return View(user);
            }
            else if (!service.IsValidMail(user)) {
                TempData["ErrorMessage"] = "This Email Already Exists, try another";
                return View(user);
            }
            else
            {
                await service.AddAsync(user);
                await LogInViaCookie(user);
            }
            return RedirectToAction("Index", "Movies", TempData["SignedIn"] = user.Name);
        }
        public async Task LogInViaCookie(User user)
        {
            string authScheme;
            ClaimsIdentity? identity;
            AuthenticationProperties authProp;
            Helper.UserCookieConfig(user, out authScheme, out identity, out authProp);

            await HttpContext.SignInAsync(authScheme, new ClaimsPrincipal(identity!), authProp);
        }


    }
}
