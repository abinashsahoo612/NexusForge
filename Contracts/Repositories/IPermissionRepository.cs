using TaskManagementSystem.Enums.Workspaces;
using TaskManagementSystem.Models.Permissions;

public interface IPermissionRepository
{
    Task<IEnumerable<Permission>> GetAllPermissionsAsync();

    Task<Permission?> GetByIdAsync(int id);

    Task<IEnumerable<WorkspaceRolePermission>> GetRolePermissionsAsync(
        int workspaceId,
        WorkspaceRole role);

    Task<bool> HasPermissionAsync(
        int workspaceId,
        WorkspaceRole role,
        string permissionName);

    Task AddRolePermissionAsync(
        WorkspaceRolePermission rolePermission);

    Task DeleteRolePermissionsAsync(
        int workspaceId,
        WorkspaceRole role);

    Task SaveChangesAsync();
}
