using TaskManagementSystem.Common.Results;
using TaskManagementSystem.DTOs.Permissions;
using TaskManagementSystem.Enums.Workspaces;

public interface IPermissionService
{
    Task<ServiceResult<IEnumerable<PermissionDto>>> GetAllPermissionsAsync();

    Task<ServiceResult<PermissionManagementDto>> GetRolePermissionsAsync(
        int workspaceId,
        WorkspaceRole role);

    Task<ServiceResult> SaveRolePermissionsAsync(
        int workspaceId,
        WorkspaceRole role,
        List<int> permissionIds,
        string currentUserId);

    Task<ServiceResult<bool>> HasPermissionAsync(
        int workspaceId,
        string userId,
        string permissionName);
}