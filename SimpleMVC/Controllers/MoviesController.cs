using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SimpleMVC.Data;
using SimpleMVC.Data.Services;
using SimpleMVC.Models;
using SimpleMVC.Models.ViewModels;
using System.Security.Claims;

namespace SimpleMVC.Controllers
{
    [Authorize]
    public class MoviesController : Controller
    {
        private readonly IEntityControllerService<Movie> movieService;
        private readonly IEntityControllerService<Producer> producerService;
        private readonly IEntityControllerService<Cinema> cinemaService;
        private readonly UserService userService;
        public MoviesController(IEntityControllerService<Movie> mov, 
            IEntityControllerService<Producer> prod,
            IEntityControllerService<Cinema> cin,
            UserService service)
        {
            movieService = mov;
            producerService = prod;
            cinemaService = cin;
            this.userService = service;
        }
        public async Task<IActionResult> Index()
        {
            var data = await movieService.GetInlcudedListAsync(m => m.Cinema!);
            string? userEmail = User.FindFirstValue(ClaimTypes.Email);
            if (userEmail != null)
            {
                User? user = await userService.GetByEmailAsync(userEmail);
                ViewBag.PictureData = user?.PictureData;
            }
            return View(data);
        }
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int id)
        {
            Movie? data = (await movieService.GetInlcudedListAsync(m => m.Cinema!))
                            .FirstOrDefault(m => m.Id == id); 
         
            if (data == null) return Redirect(nameof(Index));

            MovieViewModel movieViewModel = new MovieViewModel()
            {   Movie = data ,
                Producers = await producerService.GetAllAsync(),
                Cinemas = await cinemaService.GetAllAsync()
            };

            if (movieViewModel == null)
            {
                return Redirect(nameof(Index));
            }
            else
            {
                return View(movieViewModel!);
            }
        }
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(Movie movie, IFormFile? moviePictureData,
                                              string moviePictureDataString)
        {
            byte[]? imageData = Helper.FromImgToBytes(moviePictureData, moviePictureDataString);
            movie.PictureData = imageData;
            if (!ModelState.IsValid)
            {
                return View(movie);
            }
            else
            {
                movie.Producer = await producerService.GetByIdAsync(movie.ProducerId);
                movie.Cinema = await cinemaService.GetByIdAsync(movie.CinemaId);
                await movieService.UpdateAsync(movie);
                return RedirectToAction(nameof(Index));
            }
        }

        public async Task<IActionResult> Details(int id)
        {
            Movie? data = (await movieService.GetInlcudedListAsync(m => m.Cinema!)).
                                FirstOrDefault(m => m.Id == id);
            data = (await movieService.GetInlcudedListAsync(p => p.Producer!)).
                                  FirstOrDefault(p => p.Id == id);

            if (data == null) return Redirect(nameof(Index));
            else
            {
                return View(data);
            }
        }
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int id)
        {
            Movie? movie = await movieService.GetByIdAsync(id);
            if (movie != null)
            {
                await movieService.RemoveAsync(id);
                return RedirectToAction(nameof(Index));
            }
            return RedirectToAction(nameof(Index));

        }
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create()
        {
            await SetViewBagForMovieAdd();
            return View();
        }
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create(Movie movie, IFormFile? moviePictureData)
        {
            if (await ProcessMovieCreation(movie, moviePictureData))
            {
                return RedirectToAction("Index", "Movies", ViewData["IsMovieAdded"] = "True");
            }
            return View(movie);
        }
        public async Task<IActionResult> Filter(string? searchString)
        {
            if (searchString != null)
            {
                var movies = await movieService.GetAllAsync(); 
                var filteredMovies = movies.Where(m => m.Name!.ToLower()
                .Contains(searchString.ToLower()))
                .ToList();

                return View("Index", filteredMovies);
            }
            return RedirectToAction(nameof(Index));
        }

        private async Task<bool> ProcessMovieCreation(Movie movie, IFormFile? moviePictureData)
        {

            await SetViewBagForMovieAdd();
            if (moviePictureData == null && !(moviePictureData?.Length > 0))
            {
                movie.PictureData = Helper.DefaultImage();
            }
            else
            {
                movie.PictureData = Helper.FromImgToBytes(moviePictureData);
            }

            if (ModelState.IsValid)
            {
                movie.Producer = await producerService.GetByIdAsync(movie.ProducerId);
                movie.Cinema = await cinemaService.GetByIdAsync(movie.CinemaId);
                await movieService.AddAsync(movie);
                return true;
            }

            return false;
        }
        public async Task<IEnumerable<Producer>> GetProducersAsync()
        {
            return await producerService.GetAllAsync();
    }
        public async Task<IEnumerable<Cinema>> GetCinemasAsync()
        {
            return await cinemaService.GetAllAsync();
        }
        public async Task SetViewBagForMovieAdd()
        {
            ViewBag.Producers = await GetProducersAsync();
            ViewBag.Cinemas = await GetCinemasAsync();
        }
    }
}
