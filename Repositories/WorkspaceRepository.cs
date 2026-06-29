using Microsoft.EntityFrameworkCore;
using TaskManagementSystem.Contracts.Repositories;
using TaskManagementSystem.Data;
using TaskManagementSystem.Models.Workspaces;
using TaskManagementSystem.DTOs.Workspaces;

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

        public async Task<IEnumerable<WorkspaceListItemDto>> GetUserWorkspacesAsync(string userId)
        {
            return await _context.WorkspaceMembers
                .Where(wm => wm.UserId == userId && wm.IsActive)
                .Select(wm => new WorkspaceListItemDto
                {
                    Id = wm.WorkspaceId,
                    Name = wm.Workspace.Name,
                    Description = wm.Workspace.Description,
                    MyRole = wm.Role.ToString(),
                    MembershipPolicy = wm.Workspace.MembershipPolicy.ToString(),
                    IsActive = wm.Workspace.IsActive,
                    MemberCount = wm.Workspace.Members.Count(m => m.IsActive)
                })
                .OrderBy(w => w.Name)
                .ToListAsync();
        }
    }
}