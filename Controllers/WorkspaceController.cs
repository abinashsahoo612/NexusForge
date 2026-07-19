using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using TaskManagementSystem.Common.Results;
using TaskManagementSystem.Contracts.Services;
using TaskManagementSystem.DTOs.Task;
using TaskManagementSystem.DTOs.Workspaces;
using TaskManagementSystem.Models.Identity;

namespace TaskManagementSystem.Controllers
{
    [Authorize]
    public class WorkspaceController : Controller
    {
        private readonly IWorkspaceService _workspaceService;
        private readonly ITaskService _taskService;
        private readonly UserManager<ApplicationUser> _userManager;

        public WorkspaceController(
            IWorkspaceService workspaceService,
            ITaskService taskService,
            UserManager<ApplicationUser> userManager)
        {
            _workspaceService = workspaceService;
            _taskService = taskService;
            _userManager = userManager;
        }

        public async Task<IActionResult> Index()
        {
            var user = await _userManager.GetUserAsync(User);

            if (user == null)
                return Challenge();

            var result = await _workspaceService.GetUserWorkspacesAsync(user.Id);

            if (!result.Success)
            {
                return NotFound();
            }

            return View(result.Data);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateWorkspaceDto dto)
        {
            if (!ModelState.IsValid)
                return View(dto);

            var user = await _userManager.GetUserAsync(User);

            if (user == null)
                return Challenge();

            var result = await _workspaceService.CreateWorkspaceAsync(dto, user.Id);

            if (!result.Success)
            {
                return NotFound();
            }

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int Id)
        {
            var result = await _workspaceService.GetWorkspaceByIdAsync(Id);
            if (!result.Success)
            {
                return NotFound();
            }
            var dto = new UpdateWorkspaceDto
            {
                Id = result.Data.Id,
                Name = result.Data.Name,
                Description = result.Data.Description,
                MembershipPolicy = result.Data.MembershipPolicy
            };
            return PartialView("Partials/_EditWorkspace", dto);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(UpdateWorkspaceDto dto)
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

            var result = await _workspaceService.UpdateWorkspaceAsync(dto, user.Id);

            if (!result.Success)
            {
                return NotFound();
            }

            return Json(result);
        }

        public async Task<IActionResult> Details(int id)
        {
            var user = await _userManager.GetUserAsync(User);

            if (user == null)
                return Challenge();

            var result = await _workspaceService.GetWorkspaceDetailsAsync(id, user.Id);

            if (!result.Success)
            {
                return NotFound();
            }

            return View(result.Data);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddMember(AddWorkspaceMemberDto dto)
        {
            if (!ModelState.IsValid)
                return Json(ServiceResult.Fail("Invalid request."));

            var currentUserId = _userManager.GetUserId(User);

            if (string.IsNullOrEmpty(currentUserId))
                return Challenge();

            var result = await _workspaceService.AddMemberAsync(dto, currentUserId);

            return Json(result);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateMember(CreateWorkspaceMemberDto dto)
        {
            if (!ModelState.IsValid)
                return Json(ServiceResult.Fail("Invalid request."));

            var currentUserId = _userManager.GetUserId(User);

            if (string.IsNullOrEmpty(currentUserId))
                return Challenge();

            var result = await _workspaceService.CreateMemberAsync(dto, currentUserId);

            return Json(result);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> GetTasks(int WorkspaceId)
        {
            if (!ModelState.IsValid)
                return Json(ServiceResult.Fail("Invalid request."));

            var currentUserId = _userManager.GetUserId(User);

            if (string.IsNullOrEmpty(currentUserId))
                return Challenge();

            var result = await _workspaceService.GetTasksByWorkspaceIdAsync(WorkspaceId);

            return Json(result);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateTask(CreateTaskDto dto)
        {
            if (!ModelState.IsValid)
            {
                // reload your page/view model
            }
            var user = await _userManager.GetUserAsync(User);

            var result = await _taskService.CreateTaskAsync(dto, user.Id);

            if (!result.Success)
            {
                TempData["Error"] = result.Message;
                return RedirectToAction("Details", "Workspace", new { id = dto.WorkspaceId });
            }

            TempData["Success"] = result.Message;

            return RedirectToAction("Details", "Workspace", new { id = dto.WorkspaceId });
        }
    }
}