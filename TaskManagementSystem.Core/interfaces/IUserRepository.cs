using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagementSystem.Core.DTOs.UserDTOs;

namespace TaskManagementSystem.Core.interfaces
{
    public interface IUserRepository
    {
        IEnumerable<ApplicationUser> GetAll();
        Task<ApplicationUser> GetByID(string id);
        ApplicationUser Update(ApplicationUser user);
        void Delete(string id);
    }
}
