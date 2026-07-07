using TaskManagementSystem.Common.Results;
using TaskManagementSystem.DTOs.Task;
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
            int workspaceId);

        Task<bool> UpdateWorkspaceAsync(
            UpdateWorkspaceDto dto,
            string currentUserId);

        Task<WorkspaceDetailsDto?> GetWorkspaceDetailsAsync(int workspaceId, string currentUserId);

        Task<ServiceResult> AddMemberAsync(AddWorkspaceMemberDto dto, string currentUserId);
        Task<ServiceResult> CreateMemberAsync(CreateWorkspaceMemberDto dto, string currentUserId);
        Task<ServiceResult<TaskDto>> GetTasksByWorkspaceIdAsync(int WorkspaceId);
    }
}