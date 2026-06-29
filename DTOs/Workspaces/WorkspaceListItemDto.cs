namespace TaskManagementSystem.DTOs.Workspaces
{
    public class WorkspaceListItemDto
    {
        public int Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public string? Description { get; set; }

        public string MyRole { get; set; } = string.Empty;

        public string MembershipPolicy { get; set; } = string.Empty;

        public bool IsActive { get; set; }

        public int MemberCount { get; set; }
    }
}