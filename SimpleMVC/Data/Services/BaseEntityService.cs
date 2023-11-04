using Microsoft.EntityFrameworkCore;
using SimpleMVC.Data.Base;
using SimpleMVC.Models;
using System.Linq.Expressions;

namespace SimpleMVC.Data.Services
{
    public class BaseEntityService<T> : IEntityControllerService<T> where T : class, IBaseEntity
    {
        private readonly ApplicationContext context;
        public BaseEntityService(ApplicationContext context)
        {

            this.context = context;
        }
        public async Task AddAsync(T entity)
        {
            await context.Set<T>().AddAsync(entity);
            await context.SaveChangesAsync();
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await context.Set<T>().ToListAsync();
        }


        public async Task<T?> GetByIdAsync(int id)
        {
            return await context.Set<T>().FirstOrDefaultAsync(a => a.Id == id);
        }

        public async Task<IEnumerable<T>> GetInlcudedListAsync(Expression<Func<T, object>> includeProperty)
        {
            return await context.Set<T>().Include(includeProperty).ToListAsync();
        }

        public async Task RemoveAsync(int id)
        {
            T? entity = await context.Set<T>().FirstOrDefaultAsync(a => a.Id == id);
            context.Set<T>().Remove(entity!);
            await context.SaveChangesAsync();
        }

        public async Task<T> UpdateAsync(T newEntity)
        {
            context.Set<T>().Update(newEntity);
            await context.SaveChangesAsync();
            return newEntity;
        }
    }
}
