//To avoid ambigous error between TaskManagementSystem.Enums.TaskStatus and System.Threading.Tasks.TaskStatus


namespace TaskManagementSystem.DTOs.Task
{
    public class TaskListDto
    {
        public int? WorkspaceId { get; set; }

        public int? ProjectId { get; set; }

        public string? WorkspaceName { get; set; }

        public string? ProjectName { get; set; }

        public int TotalTasks { get; set; }

        public int PendingTasks { get; set; }

        public int InProgressTasks { get; set; }

        public int CompletedTasks { get; set; }

        public int CancelledTasks { get; set; }

        public IEnumerable<TaskListItemDto> Tasks { get; set; }
            = new List<TaskListItemDto>();
    }
}