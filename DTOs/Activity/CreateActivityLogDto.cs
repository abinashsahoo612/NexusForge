namespace TaskManagementSystem.DTOs.Activity
{
    public class CreateActivityLogDto
    {
        public string UserId { get; set; } = string.Empty;

        public int? WorkspaceId { get; set; }

        public int? ProjectId { get; set; }

        public int? TaskId { get; set; }

        public string EntityType { get; set; } = string.Empty;

        public string Action { get; set; } = string.Empty;

        public string? OldValue { get; set; }

        public string? NewValue { get; set; }

        public string? Description { get; set; }
    }
}