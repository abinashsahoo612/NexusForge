using TaskManagementSystem.DTOs.Task;
using TaskManagementSystem.DTOs.Dashboard;
using TaskManagementSystem.Common.Results;

namespace TaskManagementSystem.Contracts.Services
{
    public interface ITaskService
    {
        Task<IEnumerable<TaskDto>> GetAllTasksByUserIdAsync(string userId);
        Task<ServiceResult<TaskDto>> GetTaskByIdAsync(int id);
        Task<ServiceResult<UpdateTaskDto>> GetTaskForEditAsync(int id, string currentUserId);
        Task<ServiceResult> CreateTaskAsync(CreateTaskDto dto, string currentUserId);
        Task<ServiceResult> UpdateTaskAsync(UpdateTaskDto dto,string currentUserId);
        Task DeleteTaskAsync(int id);
        Task<DashboardDto> GetDashboardDataAsync(string userId);
    }
}