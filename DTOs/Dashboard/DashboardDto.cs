using TaskManagementSystem.DTOs.Task;

namespace TaskManagementSystem.DTOs.Dashboard
{
    public class DashboardDto
    {
        public int TotalTasks { get; set; }
        public int PendingTasks { get; set; }
        public int InProgressTasks { get; set; }
        public int CompletedTasks { get; set; }

        public List<TaskDto> RecentTasks { get; set; }

        public List<TaskDto> DueSoonTasks { get; set; }
        public List<TaskDto> OverdueTasks { get; set; }
    }
}