using TaskManagementSystem.Enums.Workspaces;
using TaskManagementSystem.DTOs.Account;

namespace TaskManagementSystem.DTOs.Workspaces
{
    public class WorkspaceDetailsDto
    {
        public int Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public string? Description { get; set; }

        public bool IsActive { get; set; }

        public DateTime CreatedAt { get; set; }

        public int MemberCount { get; set; }

        public List<WorkspaceMemberDto> Members { get; set; } = new();

        public MembershipPolicy MembershipPolicy { get; set; }

        public WorkspaceRole MyRole { get; set; }

        public List<UserListDto> AllUser {get; set;} = new();

    }
}