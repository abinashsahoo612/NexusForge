using TaskManagementSystem.Models.Identity;
using TaskManagementSystem.Models.Task;
using TaskManagementSystem.Models.Workspaces;

namespace TaskManagementSystem.Models.Activity
{
    public class ActivityLog
    {
        public long Id { get; set; }

        public string UserId { get; set; } = string.Empty;

        public ApplicationUser User { get; set; } = null!;

        public int? WorkspaceId { get; set; }

        public Workspace? Workspace { get; set; }

        public int? ProjectId { get; set; }

        public Project? Project { get; set; }

        public int? TaskId { get; set; }

        public TaskItem? Task { get; set; }

        public string EntityType { get; set; } = string.Empty;

        public string Action { get; set; } = string.Empty;

        public string? OldValue { get; set; }

        public string? NewValue { get; set; }

        public string? Description { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}