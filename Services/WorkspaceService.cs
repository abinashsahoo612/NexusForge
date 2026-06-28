using Microsoft.EntityFrameworkCore;
using TaskManagementSystem.Contracts.Repositories;
using TaskManagementSystem.Contracts.Services;
using TaskManagementSystem.Data;
using TaskManagementSystem.DTOs.Workspaces;
using TaskManagementSystem.Enums.Workspaces;
using TaskManagementSystem.Models.Workspaces;

namespace TaskManagementSystem.Services
{
    public class WorkspaceService : IWorkspaceService
    {
        private readonly IWorkspaceRepository _workspaceRepository;
        private readonly IWorkspaceMemberRepository _workspaceMemberRepository;
        private readonly ApplicationDbContext _context;

        public WorkspaceService(
            IWorkspaceRepository workspaceRepository,
            IWorkspaceMemberRepository workspaceMemberRepository,
            ApplicationDbContext context)
        {
            _workspaceRepository = workspaceRepository;
            _workspaceMemberRepository = workspaceMemberRepository;
            _context = context;
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
                    // AddedByUserId = currentUserId,
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

        public Task<IEnumerable<WorkspaceDto>> GetUserWorkspacesAsync(string userId)
        {
            throw new NotImplementedException();
        }

        public Task<WorkspaceDto?> GetWorkspaceByIdAsync(int workspaceId, string currentUserId)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateWorkspaceAsync(UpdateWorkspaceDto dto, string currentUserId)
        {
            throw new NotImplementedException();
        }
    }
}