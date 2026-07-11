namespace TaskManagementSystem.DTOs.Projects
{
    public class ProjectDetailsDto
    {
        public int Id { get; set; }

        public int WorkspaceId { get; set; }
        public string Name { get; set; } = string.Empty;

        public string? Description { get; set; }

        public bool IsActive { get; set; }

        public DateTime CreatedAt { get; set; }

        public string CreatedBy { get; set; } = string.Empty;

    }
}