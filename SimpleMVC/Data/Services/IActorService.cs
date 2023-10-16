using SimpleMVC.Models;

namespace SimpleMVC.Data.Services
{
    public interface IActorService
    {
        Task<IEnumerable<Actor>> GetAll();
        Actor GetById(int id);
        Task Add(Actor actor);
        void Remove(Actor actor);
        void Update(int id, Actor newActor);
    }
}
