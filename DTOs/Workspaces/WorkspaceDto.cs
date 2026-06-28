using TaskManagementSystem.Enums.Workspaces;

namespace TaskManagementSystem.DTOs.Workspaces
{
    public class WorkspaceDto
    {
        public int Id { get; set; }

        public string Name { get; set; } = default!;

        public string? Description { get; set; }

        public MembershipPolicy MembershipPolicy { get; set; }

        public DateTime CreatedAt { get; set; }

        public bool IsActive { get; set; }
    }
}