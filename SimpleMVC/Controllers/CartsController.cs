using Microsoft.AspNetCore.Mvc;
using SimpleMVC.Data.Enums;
using SimpleMVC.Data;
using SimpleMVC.Models;

namespace SimpleMVC.Controllers
{
    public class CartsController : Controller
    {
        public IActionResult Index()
        {
            List<Movie> moviesInCart = new List<Movie>
            {
                new Movie()
                {
                    Name = "Life",
                    Description = "This is the Life movie description",
                    Price = 39.50,
                    PictureData = Helper.FromImgToBytes("wwwroot/img/movie-1.jpeg"),
                    StartDate = DateTime.Now.AddDays(-10),
                    EndDate = DateTime.Now.AddDays(10),
                    CinemaId = 3,
                    ProducerId = 3,
                    MovieCategory = MovieCategory.Documentary
                },
                new Movie()
                {
                    Name = "The Shawshank Redemption",
                    Description = "This is the Shawshank Redemption description",
                    Price = 29.50,
                    PictureData = Helper.FromImgToBytes("wwwroot/img/movie-2.jpeg"),
                    StartDate = DateTime.Now,
                    EndDate = DateTime.Now.AddDays(3),
                    CinemaId = 1,
                    ProducerId = 1,
                    MovieCategory = MovieCategory.Action
                }
            };

            return View(moviesInCart);
        }
    }

}
