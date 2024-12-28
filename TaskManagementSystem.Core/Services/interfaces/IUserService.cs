using TaskManagementSystem.Core.DTOs.UserDTOs;

namespace TaskManagementSystem.Core.Services.interfaces
{
    public interface IUserService
    {
        IEnumerable<ApplicationUser> GetAll();
        Task<ApplicationUser> GetByID(string id);
        Task<string> Update(string id, UpdateUserDTO userDto);
        int Delete(string id);
    }
}
