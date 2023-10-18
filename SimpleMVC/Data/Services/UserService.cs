using Microsoft.EntityFrameworkCore;
using SimpleMVC.Models;
using System.Linq.Expressions;
using System.Xml.Linq;

namespace SimpleMVC.Data.Services
{
    public class UserService
    {
        private readonly ApplicationContext context;
        public UserService(ApplicationContext context)
        {

            this.context = context;
        }
        public async Task AddAsync(User user)
        {
            await context.Users.AddAsync(user);
            await context.SaveChangesAsync();
        }

        public User? GetUser(User user)
        {
            return context.Users.FirstOrDefault(u => u.Name == user.Name &&
                                                         u.Password == user.Password &&
                                                         u.Email == user.Email);
        }

        public async Task<User?> GetByIdAsync(int id)
        {
            return await context.Users.FirstOrDefaultAsync(a => a.Id == id);
        }

    }
}
