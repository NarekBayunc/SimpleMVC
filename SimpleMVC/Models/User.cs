using SimpleMVC.Data.Base;
using SimpleMVC.Data.Enums;
using System.ComponentModel.DataAnnotations;

namespace SimpleMVC.Models
{
    public class User : IBaseEntity
    {
        [Key]
        public int Id { get; set; }
        [Display(Name="Login")]
        [Required(ErrorMessage = "Name is required")]
        [MinLength(3, ErrorMessage = "Name must have at least 3 characters")]
        public string? Name { get; set; }
        [Required(ErrorMessage = "Email is required")]
        [RegularExpression(@"^[\w-]+(\.[\w-]+)*@([\w-]+\.)+[a-zA-Z]{2,7}$",
            ErrorMessage = "Invalid email address")]
        public string? Email { get; set; }
        [Required(ErrorMessage = "Password is required")]
        [MinLength(3, ErrorMessage = "Password must have at least 3 characters")]
        public string? Password { get; set; }
        [Required]
        public UserRole Role { get; set; } = UserRole.Default;
    }
}
