using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using TaskManagementSystem.Contracts.Services;
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
    }
}