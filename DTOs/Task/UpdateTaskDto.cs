
using System.ComponentModel.DataAnnotations;
using TaskStatus = TaskManagementSystem.Enums.Task.TaskStatus;
using TaskPriority = TaskManagementSystem.Enums.Task.TaskPriority;
using TaskManagementSystem.DTOs.Projects;
using TaskManagementSystem.DTOs.Account;

namespace TaskManagementSystem.DTOs.Task
{
    public class UpdateTaskDto
    {
        public int Id { get; set; }

        [Required]
        public int ProjectId { get; set; }

        [Required]
        [StringLength(200)]
        public string Title { get; set; } = string.Empty;

        [StringLength(1000)]
        public string? Description { get; set; }

        public TaskStatus Status { get; set; }

        public TaskPriority Priority { get; set; }

        public DateTime? DueDate { get; set; }

        public string? AssignedToUserId { get; set; }

        public int WorkspaceId { get; set; }

        public List<ProjectListItemDto> ProjectList { get; set; } = new();

        public List<UserListDto> UserList { get; set; } = new();
    }
}