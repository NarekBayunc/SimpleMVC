using SimpleMVC.Data.Base;

namespace SimpleMVC.Models
{
    public class Actor : AbstractPerson, IBaseEntity
    {
        public List<ActorMovie>? Actors_Movies { get; set; }
    }
}
