using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagementSystem.Core;
using TaskManagementSystem.Core.interfaces;

namespace TaskManagementSystem.EF.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext context;

        public UserRepository(ApplicationDbContext context)
        {
            this.context = context;
        }

        public async void Delete(string id)
        {
            var user = await GetByID(id);
            context.Users.Remove(user);
        }

        public IEnumerable<ApplicationUser> GetAll()
        {
            return context.Users.ToList();
        }

        public async Task<ApplicationUser> GetByID(string id)
        {
            return await context.Users.FirstOrDefaultAsync(u => u.Id == id);
        }

        public ApplicationUser Update(ApplicationUser user)
        {
            context.Users.Update(user);
            return user;
        }
    }
}
