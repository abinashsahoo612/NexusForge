using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TaskManagementSystem.Enums.Workspaces;
using TaskManagementSystem.Models.Identity;
using TaskManagementSystem.Models.Task;

namespace TaskManagementSystem.Models.Workspaces
{
    public class Project
    {
        public int Id { get; set; }

        [Required]
        public int WorkspaceId { get; set; }

        [ForeignKey(nameof(WorkspaceId))]
        public Workspace Workspace { get; set; } = null!;

        [Required]
        [MaxLength(150)]
        public string Name { get; set; } = string.Empty;

        [MaxLength(500)]
        public string? Description { get; set; }

        // User who created this workspace
        [Required]
        public string CreatedByUserId { get; set; } = string.Empty;

        [ForeignKey(nameof(CreatedByUserId))]
        public ApplicationUser CreatedByUser { get; set; } = null!;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public DateTime? UpdatedAt { get; set; }

        public bool IsActive { get; set; } = true;

        public ICollection<TaskItem> Tasks { get; set; } = new List<TaskItem>();
    }
}