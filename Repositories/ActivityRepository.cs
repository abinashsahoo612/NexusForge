using Microsoft.EntityFrameworkCore;
using TaskManagementSystem.Models.Activity;
using TaskManagementSystem.Data;

namespace TaskManagementSystem.Repositories
{
    public class ActivityRepository : IActivityRepository
    {
        private readonly ApplicationDbContext _context;

        public ActivityRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(ActivityLog activity)
        {
            await _context.ActivityLogs.AddAsync(activity);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<ActivityLog>> GetByTaskIdAsync(int taskId)
        {
            return await _context.ActivityLogs
                .Include(x => x.User)
                .Where(x => x.TaskId == taskId)
                .OrderByDescending(x => x.CreatedAt)
                .ToListAsync();
        }

        public async Task<IEnumerable<ActivityLog>> GetByProjectIdAsync(int projectId)
        {
            return await _context.ActivityLogs
                .Include(x => x.User)
                .Where(x => x.ProjectId == projectId)
                .OrderByDescending(x => x.CreatedAt)
                .ToListAsync();
        }

        public async Task<IEnumerable<ActivityLog>> GetByWorkspaceIdAsync(int workspaceId)
        {
            return await _context.ActivityLogs
                .Include(x => x.User)
                .Where(x => x.WorkspaceId == workspaceId)
                .OrderByDescending(x => x.CreatedAt)
                .ToListAsync();
        }
    }
}