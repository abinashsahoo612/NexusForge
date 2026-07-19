using TaskManagementSystem.DTOs.Workspaces;
using TaskManagementSystem.Models.Workspaces;

public interface IWorkspaceRepository
{
    Task<Workspace> CreateAsync(Workspace workspace);
    Task UpdateAsync();
    Task<Workspace?> GetByIdAsync(int id);
    Task<IEnumerable<Workspace>> GetAllAsync();
    Task<bool> ExistsAsync(int id);

    Task<bool> IsWorkspaceAdminAsync(int workspaceId, string userId);

    Task<bool> IsAlreadyMemberAsync(int workspaceId, string userId);

    Task<bool> IsMemberOfAnotherWorkspaceAsync(string userId);

    Task<IEnumerable<WorkspaceMember>> GetUserWorkspacesAsync(string userId);

    Task<Workspace?> GetWorkspaceDetailsAsync(int workspaceId, string userId);
}