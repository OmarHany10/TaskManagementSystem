using System.ComponentModel.DataAnnotations;

namespace TaskManagementSystem.Core.DTOs.UserDTOs
{
    public class AssignToRoleDTO
    {
        [Required]
        public string RoleName { get; set; }

        [Required]
        public string UserID { get; set; }
    }
}
