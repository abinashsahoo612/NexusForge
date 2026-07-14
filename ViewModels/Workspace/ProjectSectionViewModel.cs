using TaskManagementSystem.DTOs.Projects;

namespace TaskManagementSystem.ViewModels.Workspace
{
    public class ProjectSectionViewModel
    {
        public List<ProjectListItemDto> Projects { get; set; } = new();

        public List<ProjectDropdownDto> ProjectDropdown { get; set; } = new();
    }
}