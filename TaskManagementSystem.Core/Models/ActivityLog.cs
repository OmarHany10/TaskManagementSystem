using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TaskManagementSystem.Core.Models
{
    public class ActivityLog
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Action { get; set; }

        [Required]
        public DateTime TimeStamp { get; set; }

        public EntityType EntityType { get; set; }

        public int EntityID { get; set; }

        [ForeignKey(nameof(ApplicationUser))]
        public string UserID { get; set; }
        public ApplicationUser? User { get; set; }
    }
}
