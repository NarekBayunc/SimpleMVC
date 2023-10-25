namespace SimpleMVC.Models
{
    public class MovieViewModel
    {
        public required Movie Movie { get; set; }
        public required IEnumerable<Producer> Producers { get; set; }
        public required IEnumerable<Cinema> Cinemas { get; set; }
    }
}
