using TaskManagementSystem.Core.DTOs.UserDTOs;

namespace TaskManagementSystem.Core.Services.interfaces
{
    public interface IAuthService
    {
        public Task<UserDTO> Register(RegisterDTO registerDTO);
        public Task<UserDTO> Login(LoginDTO loginDTO);
        public Task<AssignRoleDTO> AssignToRole(AssignToRoleDTO assignToRoleDTO);
    }
}
