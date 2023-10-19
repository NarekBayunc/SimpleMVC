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
            user.Password = Helper.HashPassword(user.Password!);
            await context.Users.AddAsync(user);
            await context.SaveChangesAsync();
        }
        public async Task<User?> GetUser(User user)
        {
            User? targetUser = await context.Users.FirstOrDefaultAsync(u => u.Email == user.Email);

            if (targetUser?.Password == null ||
                user.Password == null ||
                !Helper.VerifyPassword(targetUser.Password, user.Password))
            {
                return null;
            }
            return targetUser;
        }
        public async Task<User?> GetByIdAsync(int id)
        {
            return await context.Users.FirstOrDefaultAsync(a => a.Id == id);
        }
    }
}
