using ClinicApp.Application.Interfaces;
using ClinicApp.Domain.Models;
using ClinicApp.Infrastucture.Data;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ClinicApp.Infrastucture.Repositories
{
    public class UserRepository(ClinicAppContext context) : IUserRepository
    {
        public async Task<User?> GetByIdAsync(int id)
        {
            return await context.Users.FindAsync(id);
        }

        public async Task<IEnumerable<User>> GetAllAsync()
        {
            return await context.Users.ToListAsync();
        }

        public async Task AddAsync(User user)
        {
            await context.Users.AddAsync(user);
        }

        public void Update(User user)
        {
            context.Users.Update(user);
        }

        public void Delete(User user)
        {
            context.Users.Remove(user);
        }

        public async Task<int> SaveChangesAsync()
        {
            return await context.SaveChangesAsync();
        }

        public async Task<User?> GetByEmailAsync(string email)
        {
            return await context.Users.FirstOrDefaultAsync(u => u.Email == email);
        }
    }
}
