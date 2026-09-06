using TaskManagementSystem.Enums.Workspaces;

namespace TaskManagementSystem.DTOs.Permissions
{
    public class PermissionManagementDto
    {
        public WorkspaceRole Role { get; set; }

        public List<PermissionDto> Permissions { get; set; } = new();

        public List<int> SelectedPermissionIds { get; set; } = new();
    }
}