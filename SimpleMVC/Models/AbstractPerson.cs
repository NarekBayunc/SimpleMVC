using SimpleMVC.Data;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace SimpleMVC.Models
{
    public abstract class AbstractPerson
    {
        public int Id { get; set; }
        [Display(Name = "Picture")]
        [AllowNull]
        public byte[]? PictureData { get; set; } = Helper.DefaultImage();
        [Display(Name = "Full Name")]
        [Required(ErrorMessage = "Full Name is required")]
        [StringLength(50, MinimumLength = 5, ErrorMessage = "Full Name must be between 3 and 50 characters ")]
        public string? FullName { get; set; }
        [Display(Name = "Biography")]
        [Required(ErrorMessage = "Biography is required")]
        public string? Bio { get; set; }
    }
}
