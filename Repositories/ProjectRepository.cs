using Microsoft.EntityFrameworkCore;
using TaskManagementSystem.Data;
using TaskManagementSystem.Models.Workspaces;

namespace TaskManagementSystem.Repositories
{
    public class ProjectRepository : IProjectRepository
    {
        private readonly ApplicationDbContext _context;

        public ProjectRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Project> CreateAsync(Project project)
        {
            await _context.Project.AddAsync(project);
            return project;
        }

        public Task UpdateAsync(Project project)
        {
            _context.Project.Update(project);
            return Task.CompletedTask;;
        }

        public async Task<Project?> GetByIdAsync(int id)
        {
            return await _context.Project
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<IEnumerable<Project>> GetByWorkspaceIdAsync(int workspaceId)
        {
            return await _context.Project
                .Where(p => p.WorkspaceId == workspaceId)
                .OrderBy(p => p.Name)
                .ToListAsync();
        }

        public async Task<bool> NameExistsAsync(int workspaceId, string name)
        {
            return await _context.Project.AnyAsync(x => x.WorkspaceId == workspaceId && x.Name == name);
        }
    }
}