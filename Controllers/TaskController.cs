using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using TaskManagementSystem.DTOs.Task;
using TaskManagementSystem.Contracts.Services;
using TaskManagementSystem.Models.Task;
using TaskManagementSystem.Models.Identity;

namespace TaskManagementSystem.Controllers
{
    [Authorize]
    public class TaskController : Controller
    {
        private readonly ITaskService _taskService;
        private readonly UserManager<ApplicationUser> _userManager;

        public TaskController(ITaskService taskService, UserManager<ApplicationUser> userManager)
        {
            _taskService = taskService;
            _userManager = userManager;
        }

        [HttpGet("Task/Index/{projectId}")]
        public async Task<IActionResult> Index(int projectId)
        {
            var user = await _userManager.GetUserAsync(User);

            if (user == null)
                return Challenge();

            var filter = new TaskFilterDto
            {
                ProjectId = projectId
            };
            
            var result = await _taskService.GetTaskListAsync(filter, user.Id);

            if (!result.Success)
                return NotFound();

            return View(result.Data);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateTaskDto dto)
        {
            if (!ModelState.IsValid)
                return View(dto);

            var user = await _userManager.GetUserAsync(User);

            await _taskService.CreateTaskAsync(dto, user.Id);

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Edit(int id)
        {
            var user = await _userManager.GetUserAsync(User);

            if (user == null)
                return Challenge();

            var result = await _taskService.GetTaskForEditAsync(id,user.Id);
            if (!result.Success)
            {
                return NotFound();
            }

            return PartialView("Partials/_EditTask", result.Data);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(UpdateTaskDto dto)
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

            var result = await _taskService.UpdateTaskAsync(dto, user.Id);

            if (!result.Success)
            {
                return BadRequest(new
                {
                    success = false,
                    errors = new[] { result.Message }
                });
            }

            return Json(result);
        }
        [HttpPost]
        public async Task<IActionResult> QuickUpdate(QuickUpdateTaskDto dto)
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

            var result = await _taskService.QuickUpdateTaskAsync(dto, user.Id);

            if (!result.Success)
            {
                return BadRequest(new
                {
                    success = false,
                    errors = new[] { result.Message }
                });
            }

            return Json(result);
        }

        public async Task<IActionResult> Details(int id)
        {
            var result = await _taskService.GetTaskDetailsAsync(id);

            if (!result.Success)
                return NotFound();

            return View(result.Data);
        }
    }
}