
using TaskManagementSystem.Models.Workspaces;

public interface IWorkspaceRepository
{
    Task<Workspace> CreateAsync(Workspace workspace);
    Task UpdateAsync(Workspace workspace);
    Task<Workspace?> GetByIdAsync(int id);
    Task<IEnumerable<Workspace>> GetAllAsync();
    Task<bool> ExistsAsync(int id);
}