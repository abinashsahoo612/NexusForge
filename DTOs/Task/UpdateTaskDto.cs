
using System.ComponentModel.DataAnnotations;
using Microsoft.Net.Http.Headers;
using TaskManagementSystem.Enums;

namespace TaskManagementSystem.DTOs.Task
{
    public class UpdateTaskDto
    {
        public int Id { get; set; }

        public string UserId { get; set;}
        public string Title { get; set; }
        public string Description { get; set; }

        public TaskManagementSystem.Enums.TaskStatus Status { get; set; }
        public TaskPriority Priority { get; set; }

        public DateTime? DueDate { get; set; }
    }
}