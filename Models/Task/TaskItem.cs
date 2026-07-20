using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TaskManagementSystem.Enums.Task;
using TaskManagementSystem.Models.Identity;
using TaskManagementSystem.Models.Workspaces;

namespace TaskManagementSystem.Models.Task
{
    public class TaskItem
    {
        public int Id { get; set; }

        [Required]
        public int ProjectId { get; set; }

        [ForeignKey(nameof(ProjectId))]
        public Project Project { get; set; } = null!;

        [Required]
        [MaxLength(200)]
        public string Title { get; set; }

        [MaxLength(1000)]
        public string Description { get; set; }

        //To avoid ambigous error between TaskManagementSystem.Enums.TaskStatus and System.Threading.Tasks.TaskStatus
        public TaskManagementSystem.Enums.Task.TaskStatus Status { get; set; } = TaskManagementSystem.Enums.Task.TaskStatus.Pending;

        public TaskPriority Priority { get; set; } = TaskPriority.Medium;

        public DateTime? DueDate { get; set; }

        [Required]
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? UpdatedAt { get; set; }

        public string? AssignedToUserId { get; set; } = string.Empty;

        [ForeignKey(nameof(AssignedToUserId))]
        public ApplicationUser? AssignedToUser { get; set; } = null!;

        [Required]
        public string CreatedByUserId { get; set; } = string.Empty;

        [ForeignKey(nameof(CreatedByUserId))]
        public ApplicationUser CreatedByUser { get; set; } = null!;
    }
}