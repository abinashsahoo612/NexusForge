using TaskManagementSystem.DTOs.Dashboard;
using TaskManagementSystem.DTOs.Workspaces;
using TaskManagementSystem.Models.Task;

namespace TaskManagementSystem.Contracts.Repositories
{
    public interface ITaskRepository
    {
        Task<IEnumerable<TaskItem>> GetAllByUserIdAsync(string userId);
        Task<TaskItem> GetByIdAsync(int id);
        Task AddAsync(TaskItem task);
        Task UpdateAsync();
        Task DeleteAsync(TaskItem task);
        Task<DashboardDto> GetDashboardDataAsync(string userId);

        Task<bool> AnyByProjectIdAsync(int projectId);
    }
}