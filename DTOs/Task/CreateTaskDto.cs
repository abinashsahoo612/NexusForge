using System.ComponentModel.DataAnnotations;

namespace TaskManagementSystem.DTOs.Task
{
    public class CreateTaskDto
    {
        [Required]
        public string Title { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public int ProjectId {get; set;}

        public string AssignedToUserId {get; set;}
        public int WorkspaceId {get; set;}

        public TaskManagementSystem.Enums.Task.TaskPriority Priority { get; set; }

        public TaskManagementSystem.Enums.Task.TaskStatus Status { get; set; }

        public DateTime? DueDate { get; set; }
    }
}