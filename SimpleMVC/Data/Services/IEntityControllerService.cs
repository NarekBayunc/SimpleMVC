using Microsoft.EntityFrameworkCore.Query;
using SimpleMVC.Data.Base;
using SimpleMVC.Models;
using System.Linq.Expressions;

namespace SimpleMVC.Data.Services
{
    public interface IEntityControllerService<T>
    {
        Task<IEnumerable<T>> GetAllAsync();
        Task<T?> GetByIdAsync(int id);
        Task<int> AddAsync(T entity);
        Task RemoveAsync(int id);
        Task<T> UpdateAsync(T newEntity);
        Task<IEnumerable<T>> GetInlcudedListAsync(Expression<Func<T, object>> includeProperty);
    }
}
