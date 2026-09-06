

namespace TaskManagementSystem.Models.Permissions
{
    public class Permission
    {
        public int Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;

        public ICollection<WorkspaceRolePermission> RolePermissions { get; set; }
            = new List<WorkspaceRolePermission>();
    }
}