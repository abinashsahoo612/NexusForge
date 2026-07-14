using TaskManagementSystem.DTOs.Workspaces;
using TaskManagementSystem.ViewModels.Task;

namespace TaskManagementSystem.ViewModels.Workspace
{
    public class WorkspaceDetailsViewModel
    {
        public WorkspaceDetailsDto WorkspaceInfo { get; set; } = new();

        public MemberSectionViewModel MemberSection { get; set; } = new();

        public ProjectSectionViewModel ProjectSection { get; set; } = new();

        public TaskSectionViewModel TaskSection { get; set; } = new();
    }
}