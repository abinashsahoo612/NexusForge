using Microsoft.EntityFrameworkCore;
using TaskManagementSystem.Contracts.Repositories;
using TaskManagementSystem.Data;
using TaskManagementSystem.Models.Workspaces;

namespace TaskManagementSystem.Repositories
{
    public class WorkspaceRepository : IWorkspaceRepository
    {
        private readonly ApplicationDbContext _context;

        public WorkspaceRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Workspace> CreateAsync(Workspace workspace)
        {
            await _context.Workspaces.AddAsync(workspace);
            return workspace;
        }

        public Task UpdateAsync(Workspace workspace)
        {
            _context.Workspaces.Update(workspace);
            return Task.CompletedTask;
        }

        public async Task<Workspace?> GetByIdAsync(int id)
        {
            return await _context.Workspaces
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<IEnumerable<Workspace>> GetAllAsync()
        {
            return await _context.Workspaces.ToListAsync();
        }

        public async Task<bool> ExistsAsync(int id)
        {
            return await _context.Workspaces.AnyAsync(x => x.Id == id);
        }
    }
}