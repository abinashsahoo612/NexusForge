using Microsoft.EntityFrameworkCore;
using TaskManagementSystem.Data;
using TaskManagementSystem.DTOs.Dashboard;
using TaskManagementSystem.DTOs.Task;
using TaskManagementSystem.Interfaces;
using TaskManagementSystem.Models;
using TaskStatus = TaskManagementSystem.Enums.TaskStatus;

namespace TaskManagementSystem.Repositories
{
    public class TaskRepository : ITaskRepository
    {
        private readonly ApplicationDbContext _context;

        public TaskRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<TaskItem>> GetAllByUserIdAsync(string userId)
        {
            return await _context.Tasks
                .Where(t => t.UserId == userId)
                .ToListAsync();
        }

        public async Task<TaskItem> GetByIdAsync(int id)
        {
            return await _context.Tasks.FindAsync(id);
        }

        public async Task AddAsync(TaskItem task)
        {
            await _context.Tasks.AddAsync(task);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(TaskItem task)
        {
            _context.Tasks.Update(task);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(TaskItem task)
        {
            _context.Tasks.Remove(task);
            await _context.SaveChangesAsync();
        }

        public async Task<DashboardDto> GetDashboardDataAsync(string userId)
{
            var tasks = await _context.Tasks
                .Where(t => t.UserId == userId)
                .ToListAsync();

            return new DashboardDto
            {
                TotalTasks = tasks.Count,
                PendingTasks = tasks.Count(t => t.Status == TaskStatus.Pending),
                InProgressTasks = tasks.Count(t => t.Status == TaskStatus.InProgress),
                CompletedTasks = tasks.Count(t => t.Status == TaskStatus.Completed),

                RecentTasks = tasks.Take(5)
                            .Select(t => new TaskDto
                            {
                                Id = t.Id,
                                Title = t.Title,
                                Status = t.Status,
                                Priority = t.Priority,
                                DueDate = t.DueDate
                            }).ToList(),
                DueSoonTasks = tasks
                                .Where(t => t.DueDate != null &&
                                            t.DueDate > DateTime.UtcNow &&
                                            t.DueDate <= DateTime.UtcNow.AddDays(3) &&
                                            t.Status != TaskStatus.Completed)
                                .OrderBy(t => t.DueDate)
                                .Select(t => new TaskDto
                                {
                                    Id = t.Id,
                                    Title = t.Title,
                                    Status = t.Status,
                                    Priority = t.Priority,
                                    DueDate = t.DueDate
                                }).ToList(),
                OverdueTasks = tasks
                                .Where(t => t.DueDate != null &&
                                            t.DueDate < DateTime.UtcNow &&
                                            t.Status != TaskStatus.Completed)
                                .OrderBy(t => t.DueDate)
                                .Select(t => new TaskDto
                                {
                                    Id = t.Id,
                                    Title = t.Title,
                                    Status = t.Status,
                                    Priority = t.Priority,
                                    DueDate = t.DueDate
                                }).ToList()
            };
        }
    }
}