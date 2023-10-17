using SimpleMVC.Models;

namespace SimpleMVC.Data.Services
{
    public interface IEntityControllerService<T>
    {
        Task<IEnumerable<T>> GetAllAsync();
        Task<T?> GetByIdAsync(int id);
        Task AddAsync(T entity);
        Task RemoveAsync(int id);
        Task<T> UpdateAsync(int id, T newEntity);
    }
}
