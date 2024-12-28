using Microsoft.AspNetCore.Identity;

using System.ComponentModel.DataAnnotations;
using TaskManagementSystem.Core.Models;


namespace TaskManagementSystem.Core
{
    public class ApplicationUser: IdentityUser
    {
        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        public ICollection<Project>? Projects { get; set; }
        public ICollection<TaskManagementSystem.Core.Models.Task>? Tasks { get; set; }
        public ICollection<Comment>? Comments { get; set; }
        public ICollection<ActivityLog>? ActivityLogs { get; set; }
    }
}
