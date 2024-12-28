using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TaskManagementSystem.Core.Models
{
    public class Project
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public DateTime StartDate { get; set; }

        [Required]
        public DateTime EndDate { get; set; }

        [ForeignKey(nameof(ApplicationUser))]
        public string CreatedByID { get; set; }
        public ApplicationUser? CreatedBy { get; set; }

        public ICollection<Task>? Tasks  { get; set; }
    }
}
