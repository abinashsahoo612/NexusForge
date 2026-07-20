using Microsoft.AspNetCore.Identity;
using TaskManagementSystem.Common.Results;
using TaskManagementSystem.Contracts.Services;
using TaskManagementSystem.Data;
using TaskManagementSystem.DTOs.Projects;
using TaskManagementSystem.DTOs.Workspaces;
using TaskManagementSystem.Models.Identity;
using TaskManagementSystem.Models.Workspaces;

namespace TaskManagementSystem.Services
{
    public class ProjectService : IProjectService
    {
        private readonly IProjectRepository _projectRepository;

        private readonly IWorkspaceRepository _workspaceRepository;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ApplicationDbContext _context;

        public ProjectService(
            IProjectRepository projectRepository,
            IWorkspaceRepository workspaceRepository,
            ApplicationDbContext context,
            UserManager<ApplicationUser> userManager)
        {
            _projectRepository = projectRepository;
            _workspaceRepository = workspaceRepository;
            _context = context;
            _userManager = userManager;
        }

        public async Task<ServiceResult> CreateProjectAsync(
            CreateProjectDto dto,
            string currentUserId)
        {
            // Check Admin Permission
            var isAdmin = await _workspaceRepository.IsWorkspaceAdminAsync(
                dto.WorkspaceId,
                currentUserId);

            if (!isAdmin)
            {
                return ServiceResult.Fail("You are not authorized to create a project.");
            }

            // Duplicate Project Name
            var projectExists = await _projectRepository.NameExistsAsync(
                dto.WorkspaceId,
                dto.Name);

            if (projectExists)
            {
                return ServiceResult.Fail("Project name already exists.");
            }

            await using var transaction = await _context.Database.BeginTransactionAsync();

            try
            {
                var project = new Project
                {
                    WorkspaceId = dto.WorkspaceId,
                    Name = dto.Name,
                    Description = dto.Description,
                    CreatedByUserId = currentUserId,
                    CreatedAt = DateTime.UtcNow,
                    IsActive = true
                };

                await _projectRepository.CreateAsync(project);

                await _context.SaveChangesAsync();

                await transaction.CommitAsync();

                return ServiceResult.Ok("Project created successfully.");
            }
            catch
            {
                await transaction.RollbackAsync();
                throw;
            }
        }

        public async Task<ServiceResult<IEnumerable<ProjectListItemDto>>> GetWorkspaceProjectsAsync(
            int workspaceId,
            string currentUserId)
        {
            var projects = await _projectRepository.GetByWorkspaceIdAsync(workspaceId);

            var result = projects.Select(p => new ProjectListItemDto
            {
                Id = p.Id,
                Name = p.Name,
                Description = p.Description,
                IsActive = p.IsActive
            });

            return ServiceResult<IEnumerable<ProjectListItemDto>>.Ok(result);
        }

        public async Task<WorkspaceDto?> GetWorkspaceByIdAsync(int workspaceId)
        {
            // return await _projectRepository.GetByIdAsync(workspaceId);
            throw new NotImplementedException();
        }

        public async Task<ServiceResult> UpdateProjectAsync(
            UpdateProjectDto dto,
            string currentUserId)
        {
            try
            {
                var project = await _projectRepository.GetProjectDetailsAsync(dto.Id);

                if (project == null)
                    return ServiceResult.Fail("Project not found.");

                project.Name = dto.Name;
                project.Description = dto.Description;

                await _projectRepository.UpdateAsync();

                return ServiceResult.Ok("Project updated successfully.");
            }
            catch (Exception)
            {
                return ServiceResult.Fail("Unable to update project.");
            }
        }

        public async Task<ServiceResult<ProjectDetailsDto>> GetProjectDetailsAsync(
            int projectId,
            string currentUserId)
        {
            var data =  await _projectRepository.GetProjectDetailsAsync(projectId);
            var dto = new ProjectDetailsDto
            {
                Id = data.Id,
                WorkspaceId = data.WorkspaceId,
                Name = data.Name,
                Description = data.Description
            };

            return ServiceResult<ProjectDetailsDto>.Ok(dto, "Workspace fetched successfully");
        }
    }
}