using  TaskManagementSystem.Models.Activity;

public interface IActivityRepository
{
    Task AddAsync(ActivityLog activity);

    Task<IEnumerable<ActivityLog>> GetByTaskIdAsync(int taskId);

    Task<IEnumerable<ActivityLog>> GetByProjectIdAsync(int projectId);

    Task<IEnumerable<ActivityLog>> GetByWorkspaceIdAsync(int workspaceId);
}