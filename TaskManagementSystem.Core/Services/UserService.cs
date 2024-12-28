using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagementSystem.Core.DTOs.UserDTOs;
using TaskManagementSystem.Core.Services.interfaces;

namespace TaskManagementSystem.Core.Services
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWork unitOfWork;

        public UserService(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }
        public  int Delete(string id)
        {
            unitOfWork.Users.Delete(id);
            return unitOfWork.save();
        }

        public IEnumerable<ApplicationUser> GetAll()
        {
            return unitOfWork.Users.GetAll();
        }

        public async Task<ApplicationUser> GetByID(string id)
        {
            var user = await unitOfWork.Users.GetByID(id);
            return user;
        }

        public async Task<string> Update(string id, UpdateUserDTO userDto)
        {
            var user = await GetByID(id);
            if (user == null)
                return "User Not Found";
            user.UserName = userDto.Usermame;
            user.FirstName = userDto.FirstName;
            user.LastName = userDto.LastName;
            user.Email = userDto.Email;
            unitOfWork.Users.Update(user);
            unitOfWork.save();
            return null;
        }
    }
}
