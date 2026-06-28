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

            // return RedirectToAction(nameof(Index));
            return RedirectToAction("Index", "Dashboard");
        }

        public IActionResult Index()
        {
            return RedirectToAction("Index", "Dashboard");
        }
    }
}