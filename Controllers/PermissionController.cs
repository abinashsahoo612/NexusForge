using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TaskManagementSystem.DTOs.Permissions;
using TaskManagementSystem.Enums.Workspaces;
using TaskManagementSystem.Contracts;

namespace TaskManagementSystem.Controllers
{
    [Authorize]
    public class PermissionController : Controller
    {
        private readonly IPermissionService _permissionService;

        public PermissionController(
            IPermissionService permissionService)
        {
            _permissionService = permissionService;
        }

        [HttpGet]
        public async Task<IActionResult> Index(
            int workspaceId,
            WorkspaceRole role = WorkspaceRole.Manager)
        {
            var result =
                await _permissionService.GetRolePermissionsAsync(
                    workspaceId,
                    role);

            if (!result.Success)
            {
                TempData["Error"] = result.Message;

                return RedirectToAction(
                    "Index",
                    "Workspace");
            }

            ViewBag.WorkspaceId = workspaceId;

            return View(result.Data);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Save(
            int workspaceId,
            WorkspaceRole role,
            List<int> permissionIds)
        {
            var currentUserId = User.FindFirst(
                System.Security.Claims.ClaimTypes.NameIdentifier
            )?.Value;

            if (string.IsNullOrEmpty(currentUserId))
                return Unauthorized();

            var result =
                await _permissionService.SaveRolePermissionsAsync(
                    workspaceId,
                    role,
                    permissionIds,
                    currentUserId);

            if (!result.Success)
            {
                TempData["Error"] = result.Message;

                return RedirectToAction(
                    nameof(Index),
                    new
                    {
                        workspaceId,
                        role
                    });
            }

            TempData["Success"] = result.Message;

            return RedirectToAction(
                nameof(Index),
                new
                {
                    workspaceId,
                    role
                });
        }
    }
}