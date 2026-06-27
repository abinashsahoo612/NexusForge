using System.ComponentModel.DataAnnotations;
using TaskManagementSystem.Enums;

namespace TaskManagementSystem.DTOs.Task
{
    public class CreateTaskDto
    {
        [Required]
        public string Title { get; set; }

        public string Description { get; set; }

        public TaskPriority Priority { get; set; }

        public DateTime? DueDate { get; set; }
    }
}