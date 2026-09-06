using TaskManagementSystem.DTOs.Activity;

namespace TaskManagementSystem.Services
{
    public class ActivityService : IActivityService
    {
        private readonly IActivityRepository _activityRepository;

        public ActivityService(IActivityRepository activityRepository)
        {
            _activityRepository = activityRepository;
        }

        // public async Task LogAsync(CreateActivityLogDto dto)
        // {
        //     var activity = new ActivityLogDto
        //     {
        //         UserId = dto.UserId,
        //         WorkspaceId = dto.WorkspaceId,
        //         ProjectId = dto.ProjectId,
        //         TaskId = dto.TaskId,
        //         EntityType = dto.EntityType,
        //         Action = dto.Action,
        //         OldValue = dto.OldValue,
        //         NewValue = dto.NewValue,
        //         Description = dto.Description,
        //         CreatedAt = DateTime.UtcNow
        //     };

        //     await _activityRepository.AddAsync(activity);
        // }

        public async Task<IEnumerable<ActivityLogDto>> GetTaskActivitiesAsync(int taskId)
        {
            var activities = await _activityRepository.GetByTaskIdAsync(taskId);

            return activities.Select(x => new ActivityLogDto
            {
                Id = x.Id,
                UserName = x.User.UserName ?? "Unknown",
                EntityType = x.EntityType,
                Action = x.Action,
                OldValue = x.OldValue,
                NewValue = x.NewValue,
                Description = x.Description ?? string.Empty,
                CreatedAt = x.CreatedAt
            });
        }

        public async Task<IEnumerable<ActivityLogDto>> GetProjectActivitiesAsync(int projectId)
        {
            var activities = await _activityRepository.GetByProjectIdAsync(projectId);

            return activities.Select(x => new ActivityLogDto
            {
                Id = x.Id,
                UserName = x.User.UserName ?? "Unknown",
                Action = x.Action,
                Description = x.Description ?? string.Empty,
                CreatedAt = x.CreatedAt
            });
        }

        public async Task<IEnumerable<ActivityLogDto>> GetWorkspaceActivitiesAsync(int workspaceId)
        {
            var activities = await _activityRepository.GetByWorkspaceIdAsync(workspaceId);

            return activities.Select(x => new ActivityLogDto
            {
                Id = x.Id,
                UserName = x.User.UserName ?? "Unknown",
                Action = x.Action,
                Description = x.Description ?? string.Empty,
                CreatedAt = x.CreatedAt
            });
        }
    }
}