using Microsoft.EntityFrameworkCore;
using SimpleMVC.Models;

namespace SimpleMVC.Data.Services
{
    public class ProducersService : IPersonService<Producer>
    {
        private readonly ApplicationContext context;
        public ProducersService(ApplicationContext context)
        {
            this.context = context;
        }
        public async Task AddAsync(Producer producer)
        {
            await context.Producers.AddAsync(producer);
            await context.SaveChangesAsync();
        }
        public async Task<IEnumerable<Producer>> GetAllAsync()
        {
            return await context.Producers.ToListAsync();
        }
        public async Task<Producer?> GetByIdAsync(int id)
        {
            return await context.Producers.FirstOrDefaultAsync(a => a.Id == id);
        }
        public async Task RemoveAsync(int id)
        {
            Producer? producer = await context.Producers.FirstOrDefaultAsync(a => a.Id == id);
            context.Producers.Remove(producer!);
            await context.SaveChangesAsync();
        }
        public async Task<Producer> UpdateAsync(int id, Producer newProducer)
        {
            context.Producers.Update(newProducer);
            await context.SaveChangesAsync();
            return newProducer;
        }
    }
}
