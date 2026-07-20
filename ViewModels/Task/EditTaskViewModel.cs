using TaskManagementSystem.DTOs.Account;
using TaskManagementSystem.DTOs.Projects;
using TaskManagementSystem.DTOs.Task;


namespace TaskManagementSystem.ViewModels.Task
{
    public class EditTaskViewModel
    {
        public UpdateTaskDto Task { get; set; } = new();

        public List<ProjectListItemDto> Projects { get; set; } = new();

        public List<UserListDto> Users { get; set; } = new();
    }
}