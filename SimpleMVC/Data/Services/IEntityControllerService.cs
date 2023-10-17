using SimpleMVC.Models;
using System.Linq.Expressions;

namespace SimpleMVC.Data.Services
{
    public interface IEntityControllerService<T>
    {
        Task<IEnumerable<T>> GetAllAsync();
        Task<T?> GetByIdAsync(int id);
        Task AddAsync(T entity);
        Task RemoveAsync(int id);
        Task<T> UpdateAsync(T newEntity);
        IEnumerable<T> GetInlcudedListAsync(Expression<Func<T, object>> includeProperty);
    }
}
