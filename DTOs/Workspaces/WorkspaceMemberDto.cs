using TaskManagementSystem.Enums.Workspaces;

namespace TaskManagementSystem.DTOs.Workspaces
{
    public class WorkspaceMemberDto
    {
        public string FullName { get; set; } = default!;

        public string Email { get; set; } = default!;

        public WorkspaceRole Role { get; set; }

        public DateTime JoinedAt { get; set; }

        public bool IsActive { get; set; }
    }
}