using TaskManagementSystem.DTOs.Dashboard;
using TaskManagementSystem.Models;

namespace TaskManagementSystem.Interfaces
{
    public interface ITaskRepository
    {
        Task<IEnumerable<TaskItem>> GetAllByUserIdAsync(string userId);
        Task<TaskItem> GetByIdAsync(int id);
        Task AddAsync(TaskItem task);
        Task UpdateAsync(TaskItem task);
        Task DeleteAsync(TaskItem task);
        Task<DashboardDto> GetDashboardDataAsync(string userId);
    }
}