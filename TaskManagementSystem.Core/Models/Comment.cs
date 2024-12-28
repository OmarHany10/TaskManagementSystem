using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TaskManagementSystem.Core.Models
{
    public class Comment
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Text { get; set; }

        [Required]
        public DateTime CreatedAt { get; set; }

        [ForeignKey(nameof(ApplicationUser))]
        public string UserID { get; set; }
        public ApplicationUser? User { get; set; }

        [ForeignKey(nameof(Task))]
        public int TaskID { get; set; }
        public Task? Task { get; set; }
    }
}
