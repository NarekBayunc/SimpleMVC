using SimpleMVC.Data;
using SimpleMVC.Data.Base;
using SimpleMVC.Data.Enums;
using SimpleMVC.Data.ValidationAttributes;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace SimpleMVC.Models
{
    public class Movie : IBaseEntity
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
        [Display(Name = "Picture")]
        [AllowNull]
        public byte[]? PictureData { get; set; } = Helper.DefaultImage();
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "Start Date")]
        [Required(ErrorMessage = "Start Date is required")]
        [DataType(DataType.Date)]
        [MaxDate(9, ErrorMessage = "Start Date must be on or before 2030-12-31")]
        public DateTime StartDate { get; set; }
        [Display(Name = "End Date")]
        [Required(ErrorMessage = "End Date is required")]
        [DataType(DataType.Date)]
        [MaxDate(9, ErrorMessage = "End Date must be on or before 2030-12-31")]
        public DateTime EndDate { get; set; }
        public MovieCategory MovieCategory { get; set; }
        public List<ActorMovie>? Actors_Movies { get; set; }
        public int CinemaId { get; set; }
        public Cinema? Cinema { get; set; }
        public int ProducerId { get; set; }
        public Producer? Producer { get; set; }

    }
}
