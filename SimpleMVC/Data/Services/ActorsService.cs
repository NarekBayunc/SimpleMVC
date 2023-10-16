using Microsoft.EntityFrameworkCore;
using SimpleMVC.Models;

namespace SimpleMVC.Data.Services
{
    public class ActorsService : IActorService
    {
        private readonly ApplicationContext context;
        public ActorsService(ApplicationContext context)
        {

            this.context = context;

        }
        public async Task Add(Actor actor)
        {
            await context.Actors.AddAsync(actor);
            await context.SaveChangesAsync();
        }
        public async Task<IEnumerable<Actor>> GetAll()
        {
            return await context.Actors.ToListAsync();
        }
        public Actor GetById(int id)
        {
            throw new NotImplementedException();
        }
        public void Remove(Actor actor)
        {
            throw new NotImplementedException();
        }
        public void Update(int id, Actor newActor)
        {
            throw new NotImplementedException();
        }
    }
}
