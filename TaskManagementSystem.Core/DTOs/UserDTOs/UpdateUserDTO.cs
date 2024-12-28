using System.ComponentModel.DataAnnotations;

namespace TaskManagementSystem.Core.DTOs.UserDTOs
{
    public class UpdateUserDTO
    {
        [Required]
        public string Usermame { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }
    }
}
