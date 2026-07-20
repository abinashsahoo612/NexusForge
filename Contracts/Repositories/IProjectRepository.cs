using TaskManagementSystem.Models.Workspaces;

public interface IProjectRepository
{
    Task<Project> CreateAsync(Project project);

    Task UpdateAsync();

    Task<Project?> GetProjectDetailsAsync(int id);

    Task<IEnumerable<Project>> GetByWorkspaceIdAsync(int workspaceId);

    Task<bool> NameExistsAsync(int workspaceId, string name);
}