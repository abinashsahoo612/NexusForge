namespace TaskManagementSystem.DTOs.Task
{
    public class QuickUpdateTaskDto
    {
        public int TaskId { get; set; }

        public string Field { get; set; } = string.Empty;

        public string? Value { get; set; }
    }
}