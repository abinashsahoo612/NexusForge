using TaskManagementSystem.Common.Results;
using TaskManagementSystem.DTOs.Permissions;
using TaskManagementSystem.Enums.Workspaces;
using TaskManagementSystem.Contracts.Repositories;

namespace TaskManagementSystem.Services
{
    public class PermissionService : IPermissionService
    {
        private readonly IPermissionRepository _permissionRepository;
        private readonly IWorkspaceRepository _workspaceRepository;
        private readonly IWorkspaceMemberRepository _workspaceMemberRepository;

        public PermissionService(
            IPermissionRepository permissionRepository,
            IWorkspaceRepository workspaceRepository,
            IWorkspaceMemberRepository workspaceMemberRepository)
        {
            _permissionRepository = permissionRepository;
            _workspaceRepository = workspaceRepository;
            _workspaceMemberRepository = workspaceMemberRepository;
        }

        public async Task<ServiceResult<IEnumerable<PermissionDto>>>
            GetAllPermissionsAsync()
        {
            var permissions =
                await _permissionRepository.GetAllPermissionsAsync();

            var result = permissions.Select(x => new PermissionDto
            {
                Id = x.Id,
                Name = x.Name,
                Description = x.Description
            });

            return ServiceResult<IEnumerable<PermissionDto>>
                .Ok(result);
        }

        public async Task<ServiceResult<PermissionManagementDto>>
            GetRolePermissionsAsync(
                int workspaceId,
                WorkspaceRole role)
        {
            var permissions =
                await _permissionRepository.GetAllPermissionsAsync();

            var rolePermissions =
                await _permissionRepository.GetRolePermissionsAsync(
                    workspaceId,
                    role);

            var result = new PermissionManagementDto
            {
                Role = role,

                Permissions = permissions
                    .Select(x => new PermissionDto
                    {
                        Id = x.Id,
                        Name = x.Name,
                        Description = x.Description
                    })
                    .ToList(),

                SelectedPermissionIds = rolePermissions
                    .Select(x => x.PermissionId)
                    .ToList()
            };

            return ServiceResult<PermissionManagementDto>
                .Ok(result);
        }

        public async Task<ServiceResult> SaveRolePermissionsAsync(
            int workspaceId,
            WorkspaceRole role,
            List<int> permissionIds,
            string currentUserId)
        {
            // Make sure the current user is actually the
            // Admin of this workspace.
            var isAdmin =
                await _workspaceRepository.IsWorkspaceAdminAsync(
                    workspaceId,
                    currentUserId);

            if (isAdmin)
                return ServiceResult.Fail(
                    "You do not have permission to manage workspace permissions.");

            // Make sure only valid permission IDs are saved.
            var allPermissions =
                await _permissionRepository.GetAllPermissionsAsync();

            var validPermissionIds = allPermissions
                .Where(x => permissionIds.Contains(x.Id))
                .Select(x => x.Id)
                .ToList();

            // Remove existing permissions for this role.
            await _permissionRepository.DeleteRolePermissionsAsync(
                workspaceId,
                role);

            // Add the newly selected permissions.
            foreach (var permissionId in validPermissionIds)
            {
                await _permissionRepository.AddRolePermissionAsync(
                    new Models.Permissions.WorkspaceRolePermission
                    {
                        WorkspaceId = workspaceId,
                        Role = role,
                        PermissionId = permissionId
                    });
            }

            await _permissionRepository.SaveChangesAsync();

            return ServiceResult.Ok(
                "Role permissions updated successfully.");
        }

        public async Task<ServiceResult<bool>> HasPermissionAsync(
            int workspaceId,
            string userId,
            string permissionName)
        {
            var isAdmin =
                await _workspaceRepository.IsWorkspaceAdminAsync(
                    workspaceId,
                    userId);

            // Admin always has access.
            if (isAdmin)
            {
                return ServiceResult<bool>.Ok(true);
            }
            
            var member = 
                await _workspaceMemberRepository.GetMemberAsync(workspaceId,userId);
            var hasPermission =
                await _permissionRepository.HasPermissionAsync(
                    workspaceId,
                    member.Role,
                    permissionName);

            return ServiceResult<bool>.Ok(hasPermission);
        }
    }
}