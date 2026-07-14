using TaskManagementSystem.DTOs.Task;
using TaskManagementSystem.DTOs.Dashboard;
using TaskManagementSystem.Contracts.Repositories;
using TaskManagementSystem.Contracts.Services;
using TaskManagementSystem.Models.Task;
using TaskManagementSystem.Common.Results;

namespace TaskManagementSystem.Services
{
    public class TaskService : ITaskService
    {
        private readonly ITaskRepository _taskRepository;
        private readonly IProjectRepository _projectRepository;

        public TaskService(ITaskRepository taskRepository, IProjectRepository projectRepository)
        {
            _taskRepository = taskRepository;
            _projectRepository = projectRepository;
        }

        public async Task<IEnumerable<TaskDto>> GetAllTasksByUserIdAsync(string userId)
        {
            var tasks = await _taskRepository.GetAllByUserIdAsync(userId);

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
            var t = await _taskRepository.GetByIdAsync(id);

            if (t == null) return null;

            return new TaskDto
            {
                Id = t.Id,
                Title = t.Title,
                Description = t.Description,
                Status = t.Status,
                Priority = t.Priority,
                DueDate = t.DueDate,
                UserId = t.AssignedToUserId
            };
        }

        public async Task<ServiceResult> CreateTaskAsync(CreateTaskDto dto, string currentUserId)
        {
            var task = new TaskItem
            {
                ProjectId = dto.ProjectId,
                Title = dto.Title,
                Description = dto.Description,
                Status = dto.Status,
                Priority = dto.Priority,
                DueDate = dto.DueDate,
                AssignedToUserId = dto.AssignedToUserId,
                CreatedByUserId = currentUserId
            };

            await _taskRepository.AddAsync(task);

            var project = await _projectRepository.GetByIdAsync(dto.ProjectId);

            dto.WorkspaceId = project.WorkspaceId;
            return ServiceResult<CreateTaskDto>.Ok(dto,"Task created successfully.");
        }

        public async Task UpdateTaskAsync(UpdateTaskDto dto)
        {
            var task = await _taskRepository.GetByIdAsync(dto.Id);

            if (task == null) return;

            task.Title = dto.Title;
            task.Description = dto.Description;
            task.Status = dto.Status;
            task.Priority = dto.Priority;
            task.DueDate = dto.DueDate;
            task.UpdatedAt = DateTime.UtcNow;

            await _taskRepository.UpdateAsync(task);
        }

        public async Task DeleteTaskAsync(int id)
        {
            var task = await _taskRepository.GetByIdAsync(id);

            if (task == null) return;

            await _taskRepository.DeleteAsync(task);
        }

        public async Task<DashboardDto> GetDashboardDataAsync(string userId)
        {
            return await _taskRepository.GetDashboardDataAsync(userId);
        }
    }
}