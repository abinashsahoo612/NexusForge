using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using TaskManagementSystem.Common.Results;
using TaskManagementSystem.Contracts.Repositories;
using TaskManagementSystem.Contracts.Services;
using TaskManagementSystem.Data;
using TaskManagementSystem.DTOs.Workspaces;
using TaskManagementSystem.Enums.Workspaces;
using TaskManagementSystem.Models.Identity;
using TaskManagementSystem.Models.Workspaces;

namespace TaskManagementSystem.Services
{
    public class WorkspaceService : IWorkspaceService
    {
        private readonly IWorkspaceRepository _workspaceRepository;
        private readonly IWorkspaceMemberRepository _workspaceMemberRepository;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ApplicationDbContext _context;

        public WorkspaceService(
            IWorkspaceRepository workspaceRepository,
            IWorkspaceMemberRepository workspaceMemberRepository,
            ApplicationDbContext context,
            UserManager<ApplicationUser> userManager)
        {
            _workspaceRepository = workspaceRepository;
            _workspaceMemberRepository = workspaceMemberRepository;
            _context = context;
            _userManager = userManager;
        }

        public async Task<WorkspaceDto> CreateWorkspaceAsync(
            CreateWorkspaceDto dto,
            string currentUserId)
        {
            await using var transaction = await _context.Database.BeginTransactionAsync();

            try
            {
                // Create Workspace
                var workspace = new Workspace
                {
                    Name = dto.Name,
                    Description = dto.Description,
                    MembershipPolicy = dto.MembershipPolicy,
                    CreatedByUserId = currentUserId,
                    CreatedAt = DateTime.UtcNow,
                    IsActive = true
                };

                await _workspaceRepository.CreateAsync(workspace);

                // Creator becomes Admin
                var member = new WorkspaceMember
                {
                    Workspace = workspace,
                    UserId = currentUserId,
                    AddedByUserId = currentUserId,
                    Role = WorkspaceRole.Admin,
                    JoinedAt = DateTime.UtcNow,
                    IsActive = true
                };

                await _workspaceMemberRepository.AddAsync(member);

                // Save everything together
                await _context.SaveChangesAsync();

                await transaction.CommitAsync();

                return new WorkspaceDto
                {
                    Id = workspace.Id,
                    Name = workspace.Name,
                    Description = workspace.Description,
                    MembershipPolicy = workspace.MembershipPolicy,
                    CreatedAt = workspace.CreatedAt,
                    // UpdatedAt = workspace.UpdatedAt,
                    IsActive = workspace.IsActive
                };
            }
            catch(Exception e)
            {
                await transaction.RollbackAsync();
                throw;
            }
        }

        public async Task<IEnumerable<WorkspaceListItemDto>> GetUserWorkspacesAsync(string userId)
        {
            return await _workspaceRepository.GetUserWorkspacesAsync(userId);
        }

        public async Task<WorkspaceDto?> GetWorkspaceByIdAsync(int workspaceId)
        {
            // return await _workspaceRepository.GetByIdAsync(workspaceId);
            throw new NotImplementedException();
        }

        public Task<bool> UpdateWorkspaceAsync(UpdateWorkspaceDto dto, string currentUserId)
        {
            throw new NotImplementedException();
        }

        public async Task<WorkspaceDetailsDto?> GetWorkspaceDetailsAsync(int workspaceId, string userId)
        {
            return await _workspaceRepository.GetWorkspaceDetailsAsync(workspaceId, userId);
        }

        public async Task<ServiceResult> AddMemberAsync(AddWorkspaceMemberDto dto, string currentUserId)
        {
            var IsAdmin = await _workspaceRepository.IsWorkspaceAdminAsync(dto.WorkspaceId, currentUserId);
            if (!IsAdmin)
            {
                return ServiceResult.Fail("You are not authorized for this operation.");
            }
            var exists = await _workspaceRepository.IsAlreadyMemberAsync(dto.WorkspaceId, dto.UserId);

            if (exists)
            {
                return ServiceResult.Fail("User is already a member of this workspace.");
            }

            var workspace = await _workspaceRepository.GetByIdAsync(dto.WorkspaceId);

            if (workspace.MembershipPolicy == MembershipPolicy.Exclusive &&
                await _workspaceRepository.IsMemberOfAnotherWorkspaceAsync(dto.UserId))
            {
                return ServiceResult.Fail("User is already a member of another workspace.");
            }

            var member = new WorkspaceMember
            {
                WorkspaceId = dto.WorkspaceId,
                UserId = dto.UserId,
                AddedByUserId = currentUserId,
                Role = dto.Role,
                JoinedAt = DateTime.UtcNow,
                IsActive = true
            };

            await _workspaceMemberRepository.AddAsync(member);
            await _context.SaveChangesAsync();

            return ServiceResult.Ok("Member added successfully.");
        }

        public async Task<ServiceResult> CreateMemberAsync(CreateWorkspaceMemberDto dto, string currentUserId)
        {
            var IsAdmin = await _workspaceRepository.IsWorkspaceAdminAsync(dto.WorkspaceId, currentUserId);
            if (!IsAdmin)
            {
                return ServiceResult.Fail("You are not authorized for this operation.");
            }

            var IsAlreadyUser = await _userManager.FindByEmailAsync(dto.Email);

            if (IsAlreadyUser != null)
            {
                return ServiceResult.Fail("An user already taken this Email.");
            }

            await using var transaction = await _context.Database.BeginTransactionAsync();

            try
            {
                var user = new ApplicationUser
                            {
                                UserName = dto.Email,
                                Email = dto.Email,
                                FullName = dto.FullName
                            };

                var result = await _userManager.CreateAsync(user, dto.Password);

                if (!result.Succeeded)
                {
                    await transaction.RollbackAsync();
                    return ServiceResult.Fail("Failed to add user.");
                }


                var member = new WorkspaceMember
                {
                    WorkspaceId = dto.WorkspaceId,
                    UserId = user.Id,
                    AddedByUserId = currentUserId,
                    Role = dto.Role,
                    JoinedAt = DateTime.UtcNow,
                    IsActive = true
                };

                await _workspaceMemberRepository.AddAsync(member);
                await _context.SaveChangesAsync();

                await transaction.CommitAsync();
                return ServiceResult.Ok("Member created successfully.");
            }
            catch(Exception e)
            {
                await transaction.RollbackAsync();
                throw;
            }
        }
    }
}