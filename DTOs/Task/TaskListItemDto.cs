using TaskStatus = TaskManagementSystem.Enums.Task.TaskStatus;
using TaskManagementSystem.Enums.Task;

namespace TaskManagementSystem.DTOs.Task
{
    public class TaskListItemDto
    {
        public int Id { get; set; }

        public string Title { get; set; } = string.Empty;

        public TaskStatus Status { get; set; }

        public TaskPriority Priority { get; set; }

        public DateTime? DueDate { get; set; }

        public string? AssignedToName { get; set; }

        public string ProjectName { get; set; } = string.Empty;
    }
}