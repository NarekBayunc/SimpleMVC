using System.ComponentModel.DataAnnotations;

namespace SimpleMVC.Models
{
    public abstract class AbstractPerson
    {
        public int Id { get; set; }
        [Display(Name = "Profile Picture ")]
        [Required(ErrorMessage = "Profile Picture is required")]
        public string? ProfilePictureURL { get; set; }
        [Display(Name = "Full Name")]
        [Required(ErrorMessage = "Full Name is required")]
        [StringLength(50, MinimumLength = 5, ErrorMessage = "Full Name must be between 3 and 50 characters ")]
        public string? FullName { get; set; }
        [Display(Name = "Biography")]
        [Required(ErrorMessage = "Biograpgy is required")]
        public string? Bio { get; set; }
    }
}
