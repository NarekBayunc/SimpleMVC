using SimpleMVC.Data;
using SimpleMVC.Data.Base;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace SimpleMVC.Models
{
    public class Cinema : IBaseEntity
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage = "Name is required")]
        [StringLength(50, MinimumLength = 5, ErrorMessage = "Name must be between 5 and 50 characters ")]
        public string? Name { get; set; }
        [Required(ErrorMessage = "Description is required")]
        public string? Description { get; set; }
        [Display(Name = "Cinema Logo")]
        [AllowNull]
        public byte[]? LogoData { get; set; } = Helper.DefaultImageForLogo();
        public List<Movie>? Movies { get; set; }

    }
}
