using TaskManagementSystem.Common.Results;
using TaskManagementSystem.DTOs.Projects;

namespace TaskManagementSystem.Contracts.Services
{
    public interface IProjectService
    {
        Task<ServiceResult> CreateProjectAsync(
            CreateProjectDto dto,
            string currentUserId);

        Task<ServiceResult<IEnumerable<ProjectListItemDto>>> GetWorkspaceProjectsAsync(
            int workspaceId,
            string currentUserId);

        Task<ServiceResult> UpdateProjectAsync(
            UpdateProjectDto dto,
            string currentUserId);

        Task<ServiceResult<ProjectDetailsDto>> GetProjectDetailsAsync(
            int projectId,
            string currentUserId);

        Task<ServiceResult> DeleteProjectAsync(int projectId, string currentUserId);
    }
}