using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace TaskManagementSystem.Core.DTOs
{
    public class TaskDTO
    {
        public string Name { get; set; }


        public string Priority { get; set; }

        public string Description { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime DueDate { get; set; }

        public DateTime? CompleteDate { get; set; }

        public int ProjectID { get; set; }

        public string? UserID { get; set; }

    }
}
