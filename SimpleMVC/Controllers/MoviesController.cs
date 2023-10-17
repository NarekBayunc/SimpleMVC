using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SimpleMVC.Data;
using SimpleMVC.Models;

namespace SimpleMVC.Controllers
{
    public class MoviesController : Controller
    {
        private readonly ApplicationContext context;
        public MoviesController(ApplicationContext context)
        {
            this.context = context;
        }
        public async Task<IActionResult> Index()
        {
            var data = await context.Movies.Include(m => m.Cinema).ToListAsync();
            return View(data);
        }
        public async Task<IActionResult> Edit(int id)
        {
            Movie? data = await context.Movies.Include(m=>m.Cinema).FirstOrDefaultAsync(m => m.Id == id);
         
            if (data == null) return Redirect("/Movies/Index");

            MovieViewModel movieViewModel = new MovieViewModel()
            {   Movie = data ,
                Producers = await context.Producers.ToListAsync(),
                Cinemas = await context.Cinemas.ToListAsync()
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
                movie.Producer = context.Producers.FirstOrDefault(p => p.Id == movie.ProducerId);
                movie.Cinema = context.Cinemas.FirstOrDefault(c => c.Id == movie.CinemaId);
                context.Movies.Update(movie);
                await context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
        }
    }

    public class MovieViewModel
    {
        public required Movie Movie { get; set; }
        public required List<Producer> Producers { get; set; }
        public required List<Cinema> Cinemas { get; set; }
    }
}
