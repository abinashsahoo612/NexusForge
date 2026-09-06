using TaskManagementSystem.Enums.Workspaces;
using TaskManagementSystem.Models.Workspaces;

namespace TaskManagementSystem.Models.Permissions
{
    public class WorkspaceRolePermission
    {
        public int Id { get; set; }

        public int WorkspaceId { get; set; }

        public Workspace Workspace { get; set; } = null!;

        public WorkspaceRole Role { get; set; }

        public int PermissionId { get; set; }

        public Permission Permission { get; set; } = null!;
    }
}