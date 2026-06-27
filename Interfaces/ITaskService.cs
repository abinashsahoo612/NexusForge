using TaskManagementSystem.DTOs.Task;
using TaskManagementSystem.DTOs.Dashboard;

namespace TaskManagementSystem.Interfaces
{
    public interface ITaskService
    {
        Task<IEnumerable<TaskDto>> GetAllTasksByUserIdAsync(string userId);
        Task<TaskDto> GetTaskByIdAsync(int id);
        Task CreateTaskAsync(CreateTaskDto dto, string userId);
        Task UpdateTaskAsync(UpdateTaskDto dto);
        Task DeleteTaskAsync(int id);
        Task<DashboardDto> GetDashboardDataAsync(string userId);
    }
}