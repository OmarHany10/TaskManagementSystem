using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TaskManagementSystem.Core.Models
{
    public class Task
    {
        [Key]
        public int Id { get; set; }
        
        [Required]
        public string Name { get; set; }

        [Required]
        public TaskStatus Status { get; set; }

        [Required]
        public TaskPriority Priority { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public DateTime StartDate { get; set; }

        [Required]
        public DateTime DueDate { get; set; }
        public DateTime? CompleteDate { get; set; }

        [ForeignKey(nameof(Project))]
        public int ProjectID { get; set; }
        public Project? Project { get; set; }

        [ForeignKey(nameof(ApplicationUser))]
        public string UserID { get; set; }
        public ApplicationUser? User { get; set; }

        public ICollection<Comment>? Comments { get; set; }
    }
}
