//To avoid ambigous error between TaskManagementSystem.Enums.TaskStatus and System.Threading.Tasks.TaskStatus
using TaskStatus = TaskManagementSystem.Enums.TaskStatus;
using TaskManagementSystem.Enums;

namespace TaskManagementSystem.DTOs.Task
{
    public class TaskDto
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public TaskStatus Status { get; set; }
        public TaskPriority Priority { get; set; }
        public DateTime? DueDate { get; set; }
    }
}