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
        private readonly UserManager<ApplicationUser> _userManager;

        public WorkspaceController(
            IWorkspaceService workspaceService,
            UserManager<ApplicationUser> userManager)
        {
            _workspaceService = workspaceService;
            _userManager = userManager;
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

            await _workspaceService.CreateWorkspaceAsync(dto, user.Id);

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Index()
        {
            var user = await _userManager.GetUserAsync(User);

            if (user == null)
                return Challenge();

            var workspaces = await _workspaceService.GetUserWorkspacesAsync(user.Id);

            return View(workspaces);
        }

        public async Task<IActionResult> Details(int id)
        {
            var user = await _userManager.GetUserAsync(User);

            if (user == null)
                return Challenge();

            var workspace = await _workspaceService.GetWorkspaceDetailsAsync(id, user.Id);

            if (workspace == null)
                return NotFound();

            return View(workspace);
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
    }
}