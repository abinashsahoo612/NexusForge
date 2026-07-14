using TaskManagementSystem.DTOs.Account;
using TaskManagementSystem.DTOs.Projects;
using TaskManagementSystem.DTOs.Task;

namespace TaskManagementSystem.ViewModels.Task
{
    public class TaskSectionViewModel
    {
        public List<UnassignedTaskListDto> UnassignedTasks { get; set; } = new();

        public List<ProjectDropdownDto> Projects { get; set; } = new();

        public List<UserListDto> Users { get; set; } = new();
    }
}