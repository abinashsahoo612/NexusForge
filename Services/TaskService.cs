using TaskManagementSystem.DTOs.Task;
using TaskManagementSystem.DTOs.Dashboard;
using TaskManagementSystem.Contracts.Repositories;
using TaskManagementSystem.Contracts.Services;
using TaskManagementSystem.Models.Task;
using TaskManagementSystem.Common.Results;
using TaskManagementSystem.DTOs.Projects;
using TaskManagementSystem.DTOs.Account;

namespace TaskManagementSystem.Services
{
    public class TaskService : ITaskService
    {
        private readonly ITaskRepository _taskRepository;
        private readonly IProjectRepository _projectRepository;
        private readonly IWorkspaceRepository _workspaceRepository;

        public TaskService(ITaskRepository taskRepository, IProjectRepository projectRepository, IWorkspaceRepository workspaceRepository)
        {
            _taskRepository = taskRepository;
            _projectRepository = projectRepository;
            _workspaceRepository = workspaceRepository;
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

        public async Task<ServiceResult<TaskDto> >GetTaskByIdAsync(int id)
        {
            var t = await _taskRepository.GetByIdAsync(id);

            if (t == null) return null;

            var dto = new TaskDto
            {
                Id = t.Id,
                Title = t.Title,
                Description = t.Description,
                Status = t.Status,
                Priority = t.Priority,
                DueDate = t.DueDate,
                UserId = t.AssignedToUserId
            };
            return ServiceResult<TaskDto>.Ok(dto,"Task created successfully.");
        }

        public async Task<ServiceResult<UpdateTaskDto>> GetTaskForEditAsync(int id, string currentUserId)
        {
            var task = await _taskRepository.GetByIdAsync(id);

            if (task == null)
                return ServiceResult<UpdateTaskDto>.Fail("Task not found.");

            var dto = new UpdateTaskDto
            {
                Id = task.Id,
                ProjectId = task.ProjectId,
                WorkspaceId = task.Project.WorkspaceId,

                Title = task.Title,
                Description = task.Description,

                Status = task.Status,
                Priority = task.Priority,

                DueDate = task.DueDate,

                AssignedToUserId = task.AssignedToUserId
            };

            var workspace = await _workspaceRepository.GetWorkspaceDetailsAsync(dto.WorkspaceId,currentUserId);

            dto.ProjectList = workspace.Projects
                .Where(p => p.IsActive)
                .Select(p => new ProjectListItemDto
                {
                    Id = p.Id,
                    Name = p.Name,
                    Description = p.Description,
                    IsActive = p.IsActive
                })
                .ToList();

            dto.UserList = workspace.Members
                .Where(m => m.IsActive)
                .Select(m => new UserListDto
                {
                    Id = m.UserId,
                    FullName = m.User.FullName,
                    Email = m.User.Email!
                })
                .ToList();

            return ServiceResult<UpdateTaskDto>.Ok(dto);
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

            var project = await _projectRepository.GetProjectDetailsAsync(dto.ProjectId);

            dto.WorkspaceId = project.WorkspaceId;
            return ServiceResult<CreateTaskDto>.Ok(dto,"Task created successfully.");
        }

        public async Task<ServiceResult> UpdateTaskAsync(UpdateTaskDto dto, string currentUserId)
        {
            var task = await _taskRepository.GetByIdAsync(dto.Id);

            task.ProjectId = dto.ProjectId;
            task.Title = dto.Title;
            task.Description = dto.Description;
            task.Status = dto.Status;
            task.Priority = dto.Priority;
            task.DueDate = dto.DueDate;
            task.AssignedToUserId = dto.AssignedToUserId;

            await _taskRepository.UpdateAsync();

            return ServiceResult.Ok("Task updated successfully.");
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