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

        public Task UpdateAsync(Workspace workspace)
        {
            _context.Workspaces.Update(workspace);
            return Task.CompletedTask;
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
        public async Task<IEnumerable<WorkspaceListItemDto>> GetUserWorkspacesAsync(string userId)
        {
            return await _context.WorkspaceMembers
                .Where(wm => wm.UserId == userId && wm.IsActive)
                .Select(wm => new WorkspaceListItemDto
                {
                    Id = wm.WorkspaceId,
                    Name = wm.Workspace.Name,
                    Description = wm.Workspace.Description,
                    MyRole = wm.Role,
                    MembershipPolicy = wm.Workspace.MembershipPolicy,
                    IsActive = wm.Workspace.IsActive,
                    MemberCount = wm.Workspace.Members.Count(m => m.IsActive)
                })
                .OrderBy(w => w.Name)
                .ToListAsync();
        }

        public async Task<WorkspaceDetailsDto?> GetWorkspaceDetailsAsync(int workspaceId, string userId)
        {
            var Users = await _userManager
                        .Users
                        .Select(u => new UserListDto
                        {
                            Id = u.Id,
                            FullName = u.FullName,
                            Email = u.Email!
                        }).ToListAsync();
            return await _context.WorkspaceMembers
                .Where(wm =>
                    wm.WorkspaceId == workspaceId &&
                    wm.UserId == userId &&
                    wm.IsActive)
                .Select(wm => new WorkspaceDetailsDto
                {
                    Id = wm.Workspace.Id,
                    Name = wm.Workspace.Name,
                    Description = wm.Workspace.Description,
                    MembershipPolicy = wm.Workspace.MembershipPolicy,
                    IsActive = wm.Workspace.IsActive,
                    CreatedAt = wm.Workspace.CreatedAt,
                    MemberCount = wm.Workspace.Members.Count(m => m.IsActive),
                    Members = wm.Workspace.Members
                                .Where(m => m.IsActive)
                                .Select(m => new WorkspaceMemberDto
                                {
                                    FullName = m.User.FullName,
                                    Email = m.User.Email!,
                                    Role = m.Role,
                                    JoinedAt = m.JoinedAt,
                                    IsActive = m.IsActive
                                })
                                .ToList(),
                    ProjectList = wm.Workspace.Projects.Select(p => new ProjectListItemDto
                                    {
                                        Name = p.Name,
                                        Description = p.Description,
                                        IsActive = p.IsActive,
                                        MyRole = wm.Role,
                                    }).ToList(),
                    MyRole = wm.Role,
                    AllUser = Users
                })
                .FirstOrDefaultAsync();
        }
    }
}