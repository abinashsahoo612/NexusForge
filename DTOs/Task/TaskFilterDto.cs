using TaskStatus = TaskManagementSystem.Enums.Task.TaskStatus;
using TaskManagementSystem.Enums.Task;

namespace TaskManagementSystem.DTOs.Task
{
    public class TaskFilterDto
    {
        public int? WorkspaceId { get; set; }

        public int? ProjectId { get; set; }

        public string? AssignedToUserId { get; set; }

        public string? CreatedByUserId { get; set; }

        public TaskStatus? Status { get; set; }

        public TaskPriority? Priority { get; set; }
    }
}