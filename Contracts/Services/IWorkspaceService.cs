using TaskManagementSystem.DTOs.Workspaces;

namespace TaskManagementSystem.Contracts.Services
{
    public interface IWorkspaceService
    {
        Task<WorkspaceDto> CreateWorkspaceAsync(
            CreateWorkspaceDto dto,
            string currentUserId);

        Task<IEnumerable<WorkspaceListItemDto>> GetUserWorkspacesAsync(
            string userId);

        Task<WorkspaceDto?> GetWorkspaceByIdAsync(
            int workspaceId,
            string currentUserId);

        Task<bool> UpdateWorkspaceAsync(
            UpdateWorkspaceDto dto,
            string currentUserId);
    }
}