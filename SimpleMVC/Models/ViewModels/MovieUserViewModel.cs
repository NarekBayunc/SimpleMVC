namespace SimpleMVC.Models.ViewModels
{
    public class MovieUserViewModel
    {
        public IEnumerable<Movie>? Movies { get; set; }
        public User? UserData { get; set; }
    }
}
