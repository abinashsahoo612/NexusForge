using TaskManagementSystem.DTOs.Task;
using TaskManagementSystem.DTOs.Dashboard;
using TaskManagementSystem.Contracts.Repositories;
using TaskManagementSystem.Contracts.Services;
using TaskManagementSystem.Models.Task;

namespace TaskManagementSystem.Services
{
    public class TaskService : ITaskService
    {
        private readonly ITaskRepository _repo;

        public TaskService(ITaskRepository repo)
        {
            _repo = repo;
        }

        public async Task<IEnumerable<TaskDto>> GetAllTasksByUserIdAsync(string userId)
        {
            var tasks = await _repo.GetAllByUserIdAsync(userId);

            return tasks.Select(t => new TaskDto
            {
                Id = t.Id,
                Title = t.Title,
                Description = t.Description,
                Status = t.Status,
                Priority = t.Priority,
                DueDate = t.DueDate
            });
        }

        public async Task<TaskDto> GetTaskByIdAsync(int id)
        {
            var t = await _repo.GetByIdAsync(id);

            if (t == null) return null;

            return new TaskDto
            {
                Id = t.Id,
                Title = t.Title,
                Description = t.Description,
                Status = t.Status,
                Priority = t.Priority,
                DueDate = t.DueDate,
                UserId = t.UserId
            };
        }

        public async Task CreateTaskAsync(CreateTaskDto dto, string userId)
        {
            var task = new TaskItem
            {
                Title = dto.Title,
                Description = dto.Description,
                Priority = dto.Priority,
                DueDate = dto.DueDate,
                UserId = userId
            };

            await _repo.AddAsync(task);
        }

        public async Task UpdateTaskAsync(UpdateTaskDto dto)
        {
            var task = await _repo.GetByIdAsync(dto.Id);

            if (task == null) return;

            task.Title = dto.Title;
            task.Description = dto.Description;
            task.Status = dto.Status;
            task.Priority = dto.Priority;
            task.DueDate = dto.DueDate;
            task.UpdatedAt = DateTime.UtcNow;

            await _repo.UpdateAsync(task);
        }

        public async Task DeleteTaskAsync(int id)
        {
            var task = await _repo.GetByIdAsync(id);

            if (task == null) return;

            await _repo.DeleteAsync(task);
        }

        public async Task<DashboardDto> GetDashboardDataAsync(string userId)
        {
            return await _repo.GetDashboardDataAsync(userId);
        }
    }
}