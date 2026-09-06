using TaskManagementSystem.DTOs.Activity;

public interface IActivityService
{
    // Task LogAsync(CreateActivityLogDto dto);

    Task<IEnumerable<ActivityLogDto>> GetTaskActivitiesAsync(int taskId);

    Task<IEnumerable<ActivityLogDto>> GetProjectActivitiesAsync(int projectId);

    Task<IEnumerable<ActivityLogDto>> GetWorkspaceActivitiesAsync(int workspaceId);
}