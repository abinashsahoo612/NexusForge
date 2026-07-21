using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using TaskManagementSystem.Common.Results;
using TaskManagementSystem.Contracts.Services;
using TaskManagementSystem.DTOs.Projects;
using TaskManagementSystem.Models.Identity;

namespace TaskManagementSystem.Controllers
{
    [Authorize]
    public class ProjectController : Controller
    {
        private readonly IProjectService _projectService;
        private readonly UserManager<ApplicationUser> _userManager;

        public ProjectController(
            IProjectService projectService,
            UserManager<ApplicationUser> userManager)
        {
            _projectService = projectService;
            _userManager = userManager;
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateProjectDto dto)
        {
            if (!ModelState.IsValid)
                return Json(ServiceResult.Fail("Invalid request."));

            var user = await _userManager.GetUserAsync(User);

            if (user == null)
                return Challenge();

            var result = await _projectService.CreateProjectAsync(dto, user.Id);

            return Json(ServiceResult.Ok(result.Message));
        }

        public async Task<IActionResult> Index(int workspaceId)
        {
            var user = await _userManager.GetUserAsync(User);

            if (user == null)
                return Challenge();

            var result = await _projectService.GetWorkspaceProjectsAsync(
                workspaceId,
                user.Id);

            if (!result.Success)
            {
                return NotFound();
            }

            return View(result.Data);
        }

        public async Task<IActionResult> Edit(int Id)
        {
            var user = await _userManager.GetUserAsync(User);

            if (user == null)
                return Challenge();

            var result = await _projectService.GetProjectDetailsAsync(Id, user.Id);
            if (!result.Success)
            {
                return NotFound();
            }
            var dto = new UpdateProjectDto
            {
                Id = result.Data.Id,
                Name = result.Data.Name,
                Description = result.Data.Description,
            };
            return PartialView("Partials/_EditProject", dto);
        }

        public async Task<IActionResult> Update(UpdateProjectDto dto)
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values
                    .SelectMany(v => v.Errors)
                    .Select(e => e.ErrorMessage)
                    .ToList();

                return BadRequest(new
                {
                    success = false,
                    errors
                });
            }

            var user = await _userManager.GetUserAsync(User);

            if (user == null)
                return Challenge();

            var project = await _projectService.UpdateProjectAsync(dto, user.Id);

            if (!project.Success)
            {
                return NotFound();
            }

            return Json(project);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            var user = await _userManager.GetUserAsync(User);

            if (user == null)
                return Challenge();

            var result = await _projectService.DeleteProjectAsync(id, user.Id);

            if (!result.Success)
                return BadRequest(new { message = result.Message });

            return Json(result);
        }

        public async Task<IActionResult> Details(int id)
        {
            var user = await _userManager.GetUserAsync(User);

            if (user == null)
                return Challenge();

            var project = await _projectService.GetProjectDetailsAsync(id, user.Id);

            if (!project.Success)
            {
                return NotFound();
            }

            return View(project.Data);
        }
    }
}