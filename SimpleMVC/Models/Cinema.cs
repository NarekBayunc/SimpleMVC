using SimpleMVC.Data.Base;
using System.ComponentModel.DataAnnotations;

namespace SimpleMVC.Models
{
    public class Cinema : IBaseEntity
    {
        public int Id { get; set; }
        [Display(Name = "Cinema Logo")]
        public string? Logo { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public List<Movie>? Movies { get; set; }

    }
}
