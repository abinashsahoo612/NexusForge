using TaskManagementSystem.DTOs.Workspaces;
using TaskManagementSystem.Models.Workspaces;

public interface IWorkspaceRepository
{
    Task<Workspace> CreateAsync(Workspace workspace);
    Task UpdateAsync(Workspace workspace);
    Task<Workspace?> GetByIdAsync(int id);
    Task<IEnumerable<Workspace>> GetAllAsync();
    Task<bool> ExistsAsync(int id);

    Task<bool> IsWorkspaceAdminAsync(int workspaceId, string userId);

    Task<bool> IsAlreadyMemberAsync(int workspaceId, string userId);

    Task<bool> IsMemberOfAnotherWorkspaceAsync(string userId);

    Task<IEnumerable<WorkspaceListItemDto>> GetUserWorkspacesAsync(string userId);

    Task<WorkspaceDetailsDto?> GetWorkspaceDetailsAsync(int workspaceId, string userId);
}