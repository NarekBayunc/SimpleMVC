using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SimpleMVC.Data.Services;
using SimpleMVC.Models;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace SimpleMVC.Controllers
{
    [Authorize]
    public class MoviesController : Controller
    {
        private readonly IEntityControllerService<Movie> movieService;
        private readonly IEntityControllerService<Producer> producerService;
        private readonly IEntityControllerService<Cinema> cinemaService;
        public MoviesController(IEntityControllerService<Movie> mov, 
            IEntityControllerService<Producer> prod,
            IEntityControllerService<Cinema> cin)
        {
            movieService = mov;
            producerService = prod;
            cinemaService = cin;
        }
        public async Task<IActionResult> Index()
        {
            var data = await movieService.GetInlcudedListAsync(m => m.Cinema!);
            return View(data);
        }
        public async Task<IActionResult> Edit(int id)
        {
            Movie? data = (await movieService.GetInlcudedListAsync(m => m.Cinema!)).FirstOrDefault(m => m.Id == id); 
         
            if (data == null) return Redirect("/Movies/Index");

            MovieViewModel movieViewModel = new MovieViewModel()
            {   Movie = data ,
                Producers = await producerService.GetAllAsync(),
                Cinemas = await cinemaService.GetAllAsync()
            };

            if (movieViewModel == null)
            {
                return Redirect("/Movies/Index");
            }
            else
            {
                return View(movieViewModel!);
            }
        }
        [HttpPost]
        public async Task<IActionResult> Edit(Movie movie)
        {
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

            if (data == null) return Redirect("/Movies/Index");
            else
            {
                return View(data);
            }
        }
        public async Task<IActionResult> Delete(int id)
        {
            Movie? movie = await movieService.GetByIdAsync(id);
            await movieService.RemoveAsync(id);
            return RedirectToAction(nameof(Index));

        }

        public async Task<IActionResult> Create()
        {
            await SetViewBagForMovieAdd();
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(Movie movie)
        {
            await SetViewBagForMovieAdd();
            if (!ModelState.IsValid)
            {
                return View(movie);
            }
            else
            {
                movie.Producer = await producerService.GetByIdAsync(movie.ProducerId);
                movie.Cinema = await cinemaService.GetByIdAsync(movie.CinemaId);
                await movieService.AddAsync(movie);
                return RedirectToAction("Index","Movies", ViewData["IsMovieAdded"] = "True");
            }
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

    public class MovieViewModel
    {
        public required Movie Movie { get; set; }
        public required IEnumerable<Producer> Producers { get; set; }
        public required IEnumerable<Cinema> Cinemas { get; set; }
    }
    
}
