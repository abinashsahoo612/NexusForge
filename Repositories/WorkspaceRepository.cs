using Microsoft.EntityFrameworkCore;
using TaskManagementSystem.Data;
using TaskManagementSystem.Models.Workspaces;
using TaskManagementSystem.DTOs.Workspaces;
using TaskManagementSystem.DTOs.Account;
using Microsoft.AspNetCore.Identity;
using TaskManagementSystem.Models.Identity;
using TaskManagementSystem.Enums.Workspaces;
using TaskManagementSystem.DTOs.Projects;

namespace TaskManagementSystem.Repositories
{
    public class WorkspaceRepository : IWorkspaceRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public WorkspaceRepository(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public async Task<Workspace> CreateAsync(Workspace workspace)
        {
            await _context.Workspaces.AddAsync(workspace);
            return workspace;
        }

        public async Task UpdateAsync()
        {
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Workspace workspace)
        {
            _context.Workspaces.Remove(workspace);
            await _context.SaveChangesAsync();
        }
        
        public async Task<Workspace?> GetByIdAsync(int workspaceId)
        {
            return await _context.Workspaces
                .FirstOrDefaultAsync(x => x.Id == workspaceId);
        }

        public async Task<IEnumerable<Workspace>> GetAllAsync()
        {
            return await _context.Workspaces.ToListAsync();
        }

        public async Task<bool> ExistsAsync(int id)
        {
            return await _context.Workspaces.AnyAsync(x => x.Id == id);
        }

        public async Task<bool> IsWorkspaceAdminAsync(int workspaceId, string userId)
        {
            return await _context.WorkspaceMembers
                .AnyAsync(x =>
                    x.WorkspaceId == workspaceId &&
                    x.UserId == userId &&
                    x.IsActive &&
                    x.Role == WorkspaceRole.Admin);
        }

        public async Task<bool> IsAlreadyMemberAsync(int workspaceId, string userId)
        {
            return await _context.WorkspaceMembers
                .AnyAsync(x =>
                    x.WorkspaceId == workspaceId &&
                    x.UserId == userId &&
                    x.IsActive);
        }

        public async Task<bool> IsMemberOfAnotherWorkspaceAsync(string userId)
        {
            return await _context.WorkspaceMembers
                .AnyAsync(x =>
                    x.UserId == userId &&
                    x.IsActive);
        }
        public async Task<IEnumerable<WorkspaceMember>> GetUserWorkspacesAsync(string userId)
        {
            return await _context.WorkspaceMembers
                .Include(wm => wm.Workspace)
                    .ThenInclude(w => w.Members)
                .Where(wm => wm.UserId == userId && wm.IsActive)
                .OrderBy(wm => wm.Workspace.Name)
                .ToListAsync();
        }

        public async Task<Workspace?> GetWorkspaceDetailsAsync(int workspaceId, string currentUserId)
        {
            return await _context.Workspaces
                    .Include(w => w.Members)
                        .ThenInclude(m => m.User)
                    .Include(w => w.Projects)
                        .ThenInclude(p => p.Tasks)
                    .FirstOrDefaultAsync(w =>
                        w.Id == workspaceId &&
                        w.Members.Any(m =>
                            m.UserId == currentUserId &&
                            m.IsActive));
        }

    }
}