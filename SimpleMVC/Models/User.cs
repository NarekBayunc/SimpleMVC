using SimpleMVC.Data;
using SimpleMVC.Data.Base;
using SimpleMVC.Data.Enums;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace SimpleMVC.Models
{
    public class User : IBaseEntity
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage = "Name is required")]
        [MinLength(3, ErrorMessage = "Name must have at least 3 characters")]
        public string? Name { get; set; }
        [Required(ErrorMessage = "Age is required")]
        [Range(6, 100, ErrorMessage = "You must be at least 6 years old")]
        public int Age { get; set; }
        [Required(ErrorMessage = "Login is required")]
        [MinLength(3, ErrorMessage = "Login must have at least 3 characters")]
        public string? Login { get; set; }
        [Required(ErrorMessage = "Email is required")]
        [RegularExpression(@"^[\w-]+(\.[\w-]+)*@([\w-]+\.)+[a-zA-Z]{2,7}$",
            ErrorMessage = "Invalid email address")]
        public string? Email { get; set; }
        [Required(ErrorMessage = "Password is required")]
        [MinLength(3, ErrorMessage = "Password must have at least 3 characters")]
        public string? Password { get; set; }
        [Required]
        public UserRole Role { get; set; } = UserRole.Default;
        [Display(Name = "Picture")]
        [AllowNull]
        public byte[]? PictureData { get; set; } = Helper.DefaultImage();
        public List<CartItem>? CartItems { get; set; }
        public decimal Money { get; set; } = 100M; 
    }
}
