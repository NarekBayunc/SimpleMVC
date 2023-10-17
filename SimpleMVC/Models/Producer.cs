using SimpleMVC.Data.Base;

namespace SimpleMVC.Models
{
    public class Producer : AbstractPerson, IBaseEntity
    {
        public List<Movie>? Movies { get; set; }
    }
}
