using TaskManagementSystem.Enums.Workspaces;
using TaskManagementSystem.DTOs.Account;
using TaskManagementSystem.DTOs.Projects;
using TaskManagementSystem.DTOs.Task;

namespace TaskManagementSystem.DTOs.Workspaces
{
    public class WorkspaceDetailsDto
    {
        public int Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public string? Description { get; set; }

        public int MemberCount { get; set; }

        public bool IsActive { get; set; }

        public DateTime CreatedAt { get; set; }

        public MembershipPolicy MembershipPolicy { get; set; }

        public WorkspaceRole MyRole { get; set; }

    }
}