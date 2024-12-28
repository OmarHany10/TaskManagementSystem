using System.ComponentModel.DataAnnotations;

namespace TaskManagementSystem.Core.DTOs.UserDTOs
{
    public class LoginDTO
    {
        [Required]
        [Display(Name = "Emair or Username")]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
