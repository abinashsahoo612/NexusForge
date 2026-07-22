//To avoid ambigous error between TaskManagementSystem.Enums.TaskStatus and System.Threading.Tasks.TaskStatus
using TaskStatus = TaskManagementSystem.Enums.Task.TaskStatus;
using TaskManagementSystem.Enums.Task;

namespace TaskManagementSystem.DTOs.Task
{
    public class TaskListDto
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;

        public string? AssignedToUserId { get; set; }
        
        public TaskStatus Status { get; set; }
        public TaskPriority Priority { get; set; }

        public string CreatedBy { get; set; } = string.Empty;
        public DateTime? DueDate { get; set; }
    }
}