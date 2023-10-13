using System.ComponentModel.DataAnnotations;

namespace SimpleMVC.Models
{
    public abstract class AbstractPerson
    {
        public int Id { get; set; }
        [Display(Name = "Profile Picture ")]
        public string? ProfilePictureURL { get; set; }
        [Display(Name = "Full Name")]
        public string? FullName { get; set; }
        [Display(Name = "Biography")]
        public string? Bio { get; set; }
    }
}
