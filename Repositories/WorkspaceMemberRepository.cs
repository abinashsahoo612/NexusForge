using Microsoft.EntityFrameworkCore;
using TaskManagementSystem.Contracts.Repositories;
using TaskManagementSystem.Data;
using TaskManagementSystem.Models.Workspaces;

namespace TaskManagementSystem.Repositories
{
    public class WorkspaceMemberRepository : IWorkspaceMemberRepository
    {
        private readonly ApplicationDbContext _context;

        public WorkspaceMemberRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<WorkspaceMember> AddAsync(WorkspaceMember member)
        {
            await _context.WorkspaceMembers.AddAsync(member);
            return member;
        }

        public Task UpdateAsync(WorkspaceMember member)
        {
            _context.WorkspaceMembers.Update(member);
            return Task.CompletedTask;
        }

        public async Task<IEnumerable<WorkspaceMember>> GetMembersAsync(int workspaceId)
        {
            return await _context.WorkspaceMembers
                .Where(x => x.WorkspaceId == workspaceId)
                .ToListAsync();
        }

        public async Task<WorkspaceMember?> GetMemberAsync(int workspaceId, string userId)
        {
            return await _context.WorkspaceMembers
                .FirstOrDefaultAsync(x =>
                    x.WorkspaceId == workspaceId &&
                    x.UserId == userId);
        }

        // public async Task<bool> IsMemberAsync(int workspaceId, string userId)
        // {
        //     return await _context.WorkspaceMembers
        //         .AnyAsync(x =>
        //             x.WorkspaceId == workspaceId &&
        //             x.UserId == userId &&
        //             x.IsActive);
        // }
    }
}