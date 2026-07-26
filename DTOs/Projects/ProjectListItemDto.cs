using TaskManagementSystem.Enums.Workspaces;

namespace TaskManagementSystem.DTOs.Projects
{
    public class ProjectListItemDto
    {
        public int Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public string? Description { get; set; }

        public WorkspaceRole MyRole { get; set; }

        public MembershipPolicy MembershipPolicy { get; set; }

        public bool IsActive { get; set; }

        public int MemberCount { get; set; }
        public int TasksCount { get; set; }
    }
}