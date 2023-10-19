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
            User? targetUser = await context.Users.FirstOrDefaultAsync(u => u.Email == user.Email
                                                                        && u.Login == user.Login);

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
        public async Task<bool> UpdateAsync(int id, User user)
        {
            var userFromDb = await context.Users.FirstOrDefaultAsync(a => a.Id == id);
            userFromDb.Login = user.Login;
            userFromDb.Name = user.Name;
            userFromDb.Age = user.Age;
            userFromDb.Email = user.Email;
            if (user != null)
            {
                context.Users.Update(userFromDb);
                await context.SaveChangesAsync();
                return true;
            }
            else 
            {
                return false;
            }
        }
        public async Task<User?> GetByEmailAsync(string email)
        {
            return await context.Users.FirstOrDefaultAsync(a => a.Email == email);
        }
    }
}
