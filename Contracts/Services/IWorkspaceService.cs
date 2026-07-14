using TaskManagementSystem.Common.Results;
using TaskManagementSystem.DTOs.Task;
using TaskManagementSystem.DTOs.Workspaces;
using TaskManagementSystem.ViewModels.Workspace;

namespace TaskManagementSystem.Contracts.Services
{
    public interface IWorkspaceService
    {
        Task<ServiceResult> CreateWorkspaceAsync(
            CreateWorkspaceDto dto,
            string currentUserId);

        Task<ServiceResult<IEnumerable<WorkspaceListItemDto>>> GetUserWorkspacesAsync(
            string userId);

        Task<WorkspaceDto?> GetWorkspaceByIdAsync(
            int workspaceId);

        Task<bool> UpdateWorkspaceAsync(
            UpdateWorkspaceDto dto,
            string currentUserId);

        Task<ServiceResult<WorkspaceDetailsViewModel>> GetWorkspaceDetailsAsync(int workspaceId, string currentUserId);

        Task<ServiceResult> AddMemberAsync(AddWorkspaceMemberDto dto, string currentUserId);
        Task<ServiceResult> CreateMemberAsync(CreateWorkspaceMemberDto dto, string currentUserId);
        Task<ServiceResult<TaskDto>> GetTasksByWorkspaceIdAsync(int WorkspaceId);
    }
}