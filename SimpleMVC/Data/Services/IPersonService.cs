using SimpleMVC.Models;

namespace SimpleMVC.Data.Services
{
    public interface IPersonService<T>
    {
        Task<IEnumerable<T>> GetAllAsync();
        Task<T?> GetByIdAsync(int id);
        Task AddAsync(T person);
        Task RemoveAsync(int id);
        Task<T> UpdateAsync(int id, T newPerson);
    }
}
