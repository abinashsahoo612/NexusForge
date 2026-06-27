using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TaskManagementSystem.Enums;

namespace TaskManagementSystem.Models
{
    public class TaskItem
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(200)]
        public string Title { get; set; }

        [MaxLength(1000)]
        public string Description { get; set; }

        //To avoid ambigous error between TaskManagementSystem.Enums.TaskStatus and System.Threading.Tasks.TaskStatus
        public TaskManagementSystem.Enums.TaskStatus Status { get; set; } = TaskManagementSystem.Enums.TaskStatus.Pending;

        public TaskPriority Priority { get; set; } = TaskPriority.Medium;

        public DateTime? DueDate { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? UpdatedAt { get; set; }

        [Required]
        public string UserId { get; set; }

        [ForeignKey("UserId")]
        public ApplicationUser User { get; set; }
    }
}