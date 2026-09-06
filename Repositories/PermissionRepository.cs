using Microsoft.EntityFrameworkCore;
using TaskManagementSystem.Data;
using TaskManagementSystem.Enums.Workspaces;
using TaskManagementSystem.Models.Permissions;

namespace TaskManagementSystem.Repositories
{
    public class PermissionRepository : IPermissionRepository
    {
        private readonly ApplicationDbContext _context;

        public PermissionRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Permission>> GetAllPermissionsAsync()
        {
            return await _context.Permissions
                .OrderBy(x => x.Id)
                .ToListAsync();
        }

        public async Task<Permission?> GetByIdAsync(int id)
        {
            return await _context.Permissions
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<IEnumerable<WorkspaceRolePermission>> GetRolePermissionsAsync(
            int workspaceId,
            WorkspaceRole role)
        {
            return await _context.WorkspaceRolePermissions
                .Where(x =>
                    x.WorkspaceId == workspaceId &&
                    x.Role == role)
                .Include(x => x.Permission)
                .ToListAsync();
        }

        public async Task<bool> HasPermissionAsync(
            int workspaceId,
            WorkspaceRole role,
            string permissionName)
        {
            return await _context.WorkspaceRolePermissions
                .AnyAsync(x =>
                    x.WorkspaceId == workspaceId &&
                    x.Role == role &&
                    x.Permission.Name == permissionName);
        }

        public async Task AddRolePermissionAsync(
            WorkspaceRolePermission rolePermission)
        {
            await _context.WorkspaceRolePermissions.AddAsync(rolePermission);
        }

        public async Task DeleteRolePermissionsAsync(
            int workspaceId,
            WorkspaceRole role)
        {
            var permissions = await _context.WorkspaceRolePermissions
                .Where(x =>
                    x.WorkspaceId == workspaceId &&
                    x.Role == role)
                .ToListAsync();

            _context.WorkspaceRolePermissions.RemoveRange(permissions);
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}