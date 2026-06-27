using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using TaskManagementSystem.Interfaces;
using TaskManagementSystem.Models;

namespace TaskManagementSystem.Controllers
{
    [Authorize]
    public class DashboardController : Controller
    {
        private readonly ITaskService _taskService;
        private readonly UserManager<ApplicationUser> _userManager;

        public DashboardController(ITaskService taskService, UserManager<ApplicationUser> userManager)
        {
            _taskService = taskService;
            _userManager = userManager;
        }

        public async Task<IActionResult> Index()
        {
            var user = await _userManager.GetUserAsync(User);

            var data = await _taskService.GetDashboardDataAsync(user.Id);

            return View(data);
        }
    }
}