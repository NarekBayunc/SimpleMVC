using SimpleMVC.Data.Base;
using System.ComponentModel.DataAnnotations;

namespace SimpleMVC.Models
{
    public class Cinema : IBaseEntity
    {
        public int Id { get; set; }
        [Display(Name = "Cinema Logo")]
        [Required(ErrorMessage = "Logo Picture is required")]
        public string? Logo { get; set; }
        [Required(ErrorMessage = "Name is required")]
        [StringLength(50, MinimumLength = 5, ErrorMessage = "Name must be between 3 and 50 characters ")]
        public string? Name { get; set; }
        [Required(ErrorMessage = "Description is required")]
        public string? Description { get; set; }
        public List<Movie>? Movies { get; set; }

    }
}
