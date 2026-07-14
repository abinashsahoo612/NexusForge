using TaskManagementSystem.DTOs.Task;
using TaskManagementSystem.DTOs.Dashboard;
using TaskManagementSystem.Common.Results;

namespace TaskManagementSystem.Contracts.Services
{
    public interface ITaskService
    {
        Task<IEnumerable<TaskDto>> GetAllTasksByUserIdAsync(string userId);
        Task<TaskDto> GetTaskByIdAsync(int id);
        Task<ServiceResult> CreateTaskAsync(CreateTaskDto dto, string currentUserId);
        Task UpdateTaskAsync(UpdateTaskDto dto);
        Task DeleteTaskAsync(int id);
        Task<DashboardDto> GetDashboardDataAsync(string userId);
    }
}