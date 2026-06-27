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

        public async Task<IActionResult> Index()
        {
            var user = await _userManager.GetUserAsync(User);

            var tasks = await _taskService.GetAllTasksByUserIdAsync(user.Id);

            return View(tasks);
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
            var task = await _taskService.GetTaskByIdAsync(id);

            var user = await _userManager.GetUserAsync(User);

            if (task == null || task.UserId != user.Id)
            {
                return NotFound();
            }

            return View(task); 
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, UpdateTaskDto dto)
        {
            if (id != dto.Id)
                return NotFound();

            var task = await _taskService.GetTaskByIdAsync(id);
            var user = await _userManager.GetUserAsync(User);

            if (task == null || task.UserId != user.Id)
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
                return View(dto);

            // var dto = new UpdateTaskDto
            // {
            //     Id = task.Id,
            //     Title = task.Title,
            //     Description = task.Description,
            //     Status = task.Status,
            //     Priority = task.Priority,
            //     DueDate = task.DueDate
            // };

            await _taskService.UpdateTaskAsync(dto);

            return RedirectToAction(nameof(Index));
        }
    }
}