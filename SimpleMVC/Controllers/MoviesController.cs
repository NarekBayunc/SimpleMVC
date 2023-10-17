using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SimpleMVC.Data;
using SimpleMVC.Data.Services;
using SimpleMVC.Models;

namespace SimpleMVC.Controllers
{
    public class MoviesController : Controller
    {
        private readonly IEntityControllerService<Movie> movieService;
        private readonly IEntityControllerService<Producer> producerService;
        private readonly IEntityControllerService<Cinema> cinemaService;
        public MoviesController(IEntityControllerService<Movie> mov, IEntityControllerService<Producer> prod,
            IEntityControllerService<Cinema> cin)
        {
            movieService = mov;
            producerService = prod;
            cinemaService = cin;
        }
        public IActionResult Index()
        {
            var data = movieService.GetInlcudedListAsync(m => m.Cinema!).ToList();
            return View(data);
        }
        public async Task<IActionResult> Edit(int id)
        {
            Movie? data = movieService.GetInlcudedListAsync(m => m.Cinema!).FirstOrDefault(m => m.Id == id); 
         
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
                movie.Cinema = await cinemaService.GetByIdAsync(movie.ProducerId);
                await movieService.UpdateAsync(movie);
                return RedirectToAction(nameof(Index));
            }
        }
    }

    public class MovieViewModel
    {
        public required Movie Movie { get; set; }
        public required IEnumerable<Producer> Producers { get; set; }
        public required IEnumerable<Cinema> Cinemas { get; set; }
    }
}
