using TaskManagementSystem.Enums.Workspaces;

namespace TaskManagementSystem.DTOs.Permissions
{
    public class RolePermissionDto
    {
        public WorkspaceRole Role { get; set; }

        public List<int> PermissionIds { get; set; } = new();
    }
}