using Microsoft.AspNetCore.Mvc;
using SimpleMVC.Data.Services;
using SimpleMVC.Models;
using System.Security.Claims;

namespace SimpleMVC.ViewComponents
{
    public class UserPictureViewComponent : ViewComponent
    {
        private readonly UserService userService;

        public UserPictureViewComponent(UserService userService)
        {
            this.userService = userService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            string? userEmail = HttpContext.User.FindFirstValue(ClaimTypes.Email);
            if (userEmail != null)
            {
                User? user = await userService.GetByEmailAsync(userEmail);
                byte[]? pictureData = user?.PictureData;
                return View(pictureData);
            }
            return View(null);
        }
    }
}
