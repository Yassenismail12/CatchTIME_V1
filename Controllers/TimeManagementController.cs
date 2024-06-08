using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;
using TESTT.Models;
using TESTT.ViewModels;
using Task = TESTT.Models.Task;

namespace TESTT.Controllers
{
    [Authorize] // Apply authorization to the entire controller
    public class TimeManagementController : Controller
    {
        private readonly CatchTIMEContext _context;

        public TimeManagementController(CatchTIMEContext context)
        {
            _context = context;
        }

        private int? GetUserIdFromSession()
        {
            return HttpContext.Session.GetInt32("UserId");
        }

        private IActionResult HandleUnauthorizedAccess()
        {
            return RedirectToAction("Login", "Account");
        }

        private async Task<List<Task>> GetTasksByUserIdAsync(int userId)
        {
            return await _context.Tasks.Where(t => t.UserId == userId).ToListAsync();
        }

        private async Task<List<Task>> GetTodayTasksByUserIdAsync(int userId)
        {
            return await _context.Tasks.Where(t => t.UserId == userId && t.TaskDate == DateTime.Today).ToListAsync();
        }

        private async Task<List<Task>> GetTomorrowTasksByUserIdAsync(int userId)
        {
            return await _context.Tasks.Where(t => t.UserId == userId && t.TaskDate == DateTime.Today.AddDays(1)).ToListAsync();
        }

        private async Task<List<Task>> GetTasksByListIdAndUserIdAsync(int listId, int userId)
        {
            var list = await _context.Lists
                .Include(l => l.Tasks)
                .FirstOrDefaultAsync(l => l.ListId == listId && l.UserId == userId);

            return (List<Task>)(list?.Tasks);
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var userId = GetUserIdFromSession();
            if (!userId.HasValue)
            {
                return HandleUnauthorizedAccess();
            }

            var lists = await _context.Lists.Where(l => l.UserId == userId).ToListAsync();
            var allTasks = await GetTasksByUserIdAsync(userId.Value);
            var todayTasks = await GetTodayTasksByUserIdAsync(userId.Value);
            var tomorrowTasks = await GetTomorrowTasksByUserIdAsync(userId.Value);

            var viewModel = new TimeManagementViewModel
            {
                AllLists = lists,
                AllTasks = allTasks,
                TodayTasks = todayTasks,
                TomorrowTasks = tomorrowTasks,
                AllListId = -1,
                TodayListId = -2,
                TomorrowListId = -3
            };

            return View(viewModel);
        }

        [HttpGet]
        public async Task<IActionResult> AllTasks()
        {
            var userId = GetUserIdFromSession();
            if (!userId.HasValue)
            {
                return HandleUnauthorizedAccess();
            }

            var allTasks = await GetTasksByUserIdAsync(userId.Value);
            return PartialView("_ListTasksPartial", allTasks);
        }

        // Implement other actions similarly...

        [HttpPost]
        public IActionResult CreateList([FromBody] List newList)
        {
            var userId = GetUserIdFromSession();
            if (!userId.HasValue)
            {
                return HandleUnauthorizedAccess();
            }

            newList.UserId = userId.Value;
            _context.Lists.Add(newList);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        // Implement other actions similarly...

        [HttpPost]
        public IActionResult EditTask([FromBody] Task updatedTask)
        {
            var task = _context.Tasks.FirstOrDefault(t => t.TaskId == updatedTask.TaskId);
            if (task == null)
            {
                return NotFound();
            }

            task.TaskTitle = updatedTask.TaskTitle;
            task.TaskPriority = updatedTask.TaskPriority;
            // Update other task properties similarly

            _context.SaveChanges();
            return Json(new { success = true });
        }

        // Implement other actions similarly...

        [HttpPost]
        public async Task<IActionResult> UpdateTaskStatus(int id, bool isChecked)
        {
            var task = await _context.Tasks.FirstOrDefaultAsync(t => t.TaskId == id);
            if (task == null)
            {
                return NotFound();
            }

            task.TaskStatus = isChecked;
            await _context.SaveChangesAsync();

            return Json(new { success = true });
        }
    }
}
