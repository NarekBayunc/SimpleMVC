using SimpleMVC.Models;

namespace SimpleMVC.Data.Services
{
    public interface IActorService
    {
        Task<IEnumerable<Actor>> GetAllAsync();
        Task<Actor?> GetByIdAsync(int id);
        Task AddAsync(Actor actor);
        Task RemoveAsync(int id);
        Task<Actor> UpdateAsync(int id, Actor newActor);
    }
}
