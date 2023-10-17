using Microsoft.EntityFrameworkCore;
using SimpleMVC.Models;

namespace SimpleMVC.Data.Services
{
    public class ActorsService : IPersonService<Actor>
    {
        private readonly ApplicationContext context;
        public ActorsService(ApplicationContext context)
        {

            this.context = context;

        }
        public async Task AddAsync(Actor actor)
        {
            await context.Actors.AddAsync(actor);
            await context.SaveChangesAsync();
        }
        public async Task<IEnumerable<Actor>> GetAllAsync()
        {
            return await context.Actors.ToListAsync();
        }
        public async Task<Actor?> GetByIdAsync(int id)
        {
            return await context.Actors.FirstOrDefaultAsync(a => a.Id == id);
        }
        public async Task RemoveAsync(int id)
        {
            Actor? actor = await context.Actors.FirstOrDefaultAsync(a => a.Id == id);
            context.Actors.Remove(actor!);
            await context.SaveChangesAsync();
        }
        public async Task<Actor> UpdateAsync(int id, Actor newActor)
        {
            context.Actors.Update(newActor);
            await context.SaveChangesAsync();
            return newActor;
        }
    }
}
