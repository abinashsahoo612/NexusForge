namespace TaskManagementSystem.DTOs.Activity
{
    public class ActivityLogDto
    {
        public long Id { get; set; }

        public string UserName { get; set; } = string.Empty;

        public string EntityType { get; set; } = string.Empty;

        public string Action { get; set; } = string.Empty;

        public string? OldValue { get; set; }

        public string? NewValue { get; set; }

        public string Description { get; set; } = string.Empty;

        public DateTime CreatedAt { get; set; }
    }
}