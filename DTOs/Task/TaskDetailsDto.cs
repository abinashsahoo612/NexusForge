namespace TaskManagementSystem.DTOs.Task
{
    public class TaskDetailsDto
    {
        public int Id { get; set; }

        public string Title { get; set; } = string.Empty;

        public string? Description { get; set; }

        public TaskManagementSystem.Enums.Task.TaskStatus Status { get; set; }

        public TaskManagementSystem.Enums.Task.TaskPriority Priority { get; set; }

        public DateTime? DueDate { get; set; }

        public string? AssignedToUserId { get; set; }

        public string? AssignedToName { get; set; }

        public string CreatedByUserId { get; set; } = string.Empty;

        public string CreatedByName { get; set; } = string.Empty;

        public int ProjectId { get; set; }

        public string ProjectName { get; set; } = string.Empty;

        public int WorkspaceId { get; set; }

        public string WorkspaceName { get; set; } = string.Empty;

        public DateTime CreatedAt { get; set; }

        public DateTime? UpdatedAt { get; set; }
    }
}