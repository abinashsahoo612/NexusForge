using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using TaskManagementSystem.Common.Results;
using TaskManagementSystem.Contracts.Repositories;
using TaskManagementSystem.Contracts.Services;
using TaskManagementSystem.Data;
using TaskManagementSystem.DTOs.Account;
using TaskManagementSystem.DTOs.Projects;
using TaskManagementSystem.DTOs.Task;
using TaskManagementSystem.DTOs.Workspaces;
using TaskManagementSystem.Enums.Workspaces;
using TaskManagementSystem.Models.Identity;
using TaskManagementSystem.Models.Workspaces;
using TaskManagementSystem.ViewModels.Task;
using TaskManagementSystem.ViewModels.Workspace;

namespace TaskManagementSystem.Services
{
    public class WorkspaceService : IWorkspaceService
    {
        private readonly IWorkspaceRepository _workspaceRepository;
        private readonly IWorkspaceMemberRepository _workspaceMemberRepository;

        private readonly IProjectRepository _projectRepository;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ApplicationDbContext _context;

        public WorkspaceService(
            IWorkspaceRepository workspaceRepository,
            IWorkspaceMemberRepository workspaceMemberRepository,
            IProjectRepository projectRepository,
            ApplicationDbContext context,
            UserManager<ApplicationUser> userManager)
        {
            _projectRepository = projectRepository;
            _workspaceRepository = workspaceRepository;
            _workspaceMemberRepository = workspaceMemberRepository;
            _context = context;
            _userManager = userManager;
        }

        public async Task<ServiceResult> CreateWorkspaceAsync(
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

                var responseDto =  new WorkspaceDto
                    {
                        Id = workspace.Id,
                        Name = workspace.Name,
                        Description = workspace.Description,
                        MembershipPolicy = workspace.MembershipPolicy,
                        CreatedAt = workspace.CreatedAt,
                        // UpdatedAt = workspace.UpdatedAt,
                        IsActive = workspace.IsActive
                    };
                return ServiceResult.Ok("Workspace created successfully.");
            }
            catch(Exception)
            {
                await transaction.RollbackAsync();
                throw;
            }
        }

        public async Task<ServiceResult<IEnumerable<WorkspaceListItemDto>>> GetUserWorkspacesAsync(string userId)
        {
            var workspaceMembers = await _workspaceRepository
                .GetUserWorkspacesAsync(userId);

            var dto = workspaceMembers.Select(wm => new WorkspaceListItemDto
            {
                Id = wm.WorkspaceId,
                Name = wm.Workspace.Name,
                Description = wm.Workspace.Description,
                MyRole = wm.Role,
                MembershipPolicy = wm.Workspace.MembershipPolicy,
                IsActive = wm.Workspace.IsActive,
                MemberCount = wm.Workspace.Members.Count(m => m.IsActive)
            }).ToList();

            return ServiceResult<IEnumerable<WorkspaceListItemDto>>
                .Ok(dto, "Workspaces loaded successfully.");
        }

        public async Task<ServiceResult<WorkspaceDto>> GetWorkspaceByIdAsync(int workspaceId)
        {
            var data =  await _workspaceRepository.GetByIdAsync(workspaceId);
            var dto = new WorkspaceDto
            {
                Id = data.Id,
                Name = data.Name,
                Description = data.Description,
                MembershipPolicy = data.MembershipPolicy
            };

            return ServiceResult<WorkspaceDto>.Ok(dto, "Workspace fetched successfully");
            // throw new NotImplementedException();
        }

        public async Task<ServiceResult> UpdateWorkspaceAsync(UpdateWorkspaceDto dto, string currentUserId)
        {
            try
            {
                var workspace = await _workspaceRepository.GetByIdAsync(dto.Id);

                if (workspace == null)
                    return ServiceResult.Fail("Workspace not found.");

                workspace.Name = dto.Name;
                workspace.Description = dto.Description;
                workspace.MembershipPolicy = dto.MembershipPolicy;

                await _workspaceRepository.UpdateAsync();

                return ServiceResult.Ok("Workspace updated successfully.");
            }
            catch (Exception)
            {
                return ServiceResult.Fail("Unable to update workspace.");
            }
        }

        public async Task<ServiceResult<WorkspaceDetailsViewModel>> GetWorkspaceDetailsAsync(
            int workspaceId,
            string currentUserId)
        {
            var workspace = await _workspaceRepository
                .GetWorkspaceDetailsAsync(workspaceId, currentUserId);

            if (workspace == null)
            {
                return ServiceResult<WorkspaceDetailsViewModel>.Fail(
                    "Workspace not found or you don't have permission.");
            }

            var currentMember = workspace.Members
                .First(x => x.UserId == currentUserId);

            var users = await _userManager.Users
                .Select(u => new UserListDto
                {
                    Id = u.Id,
                    FullName = u.FullName,
                    Email = u.Email!
                })
                .ToListAsync();
            var workspaceDto = new WorkspaceDetailsDto
            {
                Id = workspace.Id,
                Name = workspace.Name,
                Description = workspace.Description,
                MembershipPolicy = workspace.MembershipPolicy,
                IsActive = workspace.IsActive,
                CreatedAt = workspace.CreatedAt,
                MyRole = currentMember.Role,
                MemberCount = workspace.Members.Count(m => m.IsActive),
            };

            var taskSection = new TaskSectionViewModel
            {
                Users = workspace.Members
                .Where(m => m.IsActive)
                .Select(m => new UserListDto
                {
                    Id = m.UserId,
                    FullName = m.User.FullName,
                    Email = m.User.Email!
                })
                .ToList(),

                UnassignedTasks = workspace.Projects
                    .SelectMany(p => p.Tasks)
                    .Where(t => t.AssignedToUserId == null)
                    .Select(t => new UnassignedTaskListDto
                    {
                        Id = t.Id,
                        Title = t.Title,
                        Description = t.Description,
                        Status = t.Status,
                        Priority = t.Priority,
                        CreatedBy = t.CreatedByUser.FullName
                    })
                    .ToList(),
                
                Projects = workspace.Projects
                    .Select(p => new ProjectDropdownDto
                    {
                        Id = p.Id,
                        Name = p.Name
                    })
                    .ToList(),
            };

            var projectSection = new ProjectSectionViewModel
            {
                Projects = workspace.Projects
                        .Select(p => new ProjectListItemDto
                        {
                            Id = p.Id,
                            Name = p.Name,
                            Description = p.Description,
                            IsActive = p.IsActive,
                            MyRole = currentMember.Role
                        })
                        .ToList(),
                ProjectDropdown = workspace.Projects
                    .Select(p => new ProjectDropdownDto
                    {
                        Id = p.Id,
                        Name = p.Name
                    })
                    .ToList(),
            };

            var memberSection = new MemberSectionViewModel
            {
                MemberCount = workspace.Members.Count(m => m.IsActive),
                Members = workspace.Members
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
                Users = users
            };

            var vm = new WorkspaceDetailsViewModel
            {
                WorkspaceInfo = workspaceDto,
                TaskSection = taskSection,
                ProjectSection = projectSection,
                MemberSection = memberSection,
            };

            return ServiceResult<WorkspaceDetailsViewModel>.Ok(
                vm,
                "Workspace loaded successfully.");
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

        public async Task<ServiceResult> DeleteWorkspaceAsync(int workspaceId, string currentUserId)
        {
            var workspace = await _workspaceRepository.GetByIdAsync(workspaceId);

            if (workspace == null)
                return ServiceResult.Fail("Workspace not found.");

            // Authorization
            if (workspace.CreatedByUserId != currentUserId)
                return ServiceResult.Fail("You are not authorized to delete this workspace.");

            // Business Rule
            if (await _projectRepository.AnyByWorkspaceIdAsync(workspaceId))
            {
                return ServiceResult.Fail(
                    "This workspace contains projects. Delete all projects before deleting the workspace."
                );
            }

            await _workspaceRepository.DeleteAsync(workspace);

            return ServiceResult.Ok("Workspace deleted successfully.");
        }
    
        public async Task<ServiceResult<TaskDto>> GetTasksByWorkspaceIdAsync(int WorkspaceId)
        {
            throw new NotImplementedException();
        }
    }
}