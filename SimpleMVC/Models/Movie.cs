using SimpleMVC.Data.Base;
using SimpleMVC.Data.Enums;
using System.ComponentModel.DataAnnotations;

namespace SimpleMVC.Models
{
    public class Movie
    {
        public int Id { get; set; }
        [Display(Name = "Name")]
        [Required(ErrorMessage = "Name is required")]
        [StringLength(35, MinimumLength = 3, ErrorMessage = "Name must be between 3 and 35 characters ")]
        public string? Name { get; set; }
        [Display(Name = "Description")]
        [Required(ErrorMessage = "Description is required")]
        public string? Description { get; set; }
        [Display(Name = "Price")]
        [Required(ErrorMessage = "Price is required")]
        [Range(1, 150, ErrorMessage = "Price can be between 1 and 150")]
        public double Price { get; set; }
        [Display(Name = "Movie Picture ")]
        [Required(ErrorMessage = "Movie Picture is required")]
        public string? ImageUrl { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public MovieCategory MovieCategory { get; set; }
        public List<ActorMovie>? Actors_Movies { get; set; }
        public int CinemaId { get; set; }
        public Cinema? Cinema { get; set; }
        public int ProducerId { get; set; }
        public Producer? Producer { get; set; }

    }
}
