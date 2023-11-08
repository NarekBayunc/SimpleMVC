using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;

namespace SimpleMVC.Data.Extensions
{
    public static class ControllerExtensions
    {
        public static async Task<T?> GetObjectFromDbOrCache<T>(this Controller controller, int id,
                                            Func<int, Task<T?>> getFromDatabase, IMemoryCache cache)
        {
            cache.TryGetValue(id, out T? obj);
            if (obj == null)
            {
                obj = await getFromDatabase(id);
                if (obj == null)
                {
                    return default;
                }
                cache.Set(id, obj, new MemoryCacheEntryOptions().SetAbsoluteExpiration(TimeSpan.FromMinutes(30)));

            }
            return obj;
        }
        public static async Task<T?> GetObjectFromDbOrCache<T>(this Controller controller, string email,
                                            Func<string, Task<T?>> getFromDatabase, IMemoryCache cache)
        {
            cache.TryGetValue(email, out T? obj);
            if (obj == null)
            {
                obj = await getFromDatabase(email);
                if (obj == null)
                {
                    return default; 
                }
                cache.Set(email, obj, new MemoryCacheEntryOptions().SetAbsoluteExpiration(TimeSpan.FromMinutes(30)));
            }
            return obj;
        }
    }
}
