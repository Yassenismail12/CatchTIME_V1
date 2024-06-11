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
        
        [HttpGet]
        public async Task<JsonResult> GetTasksByList(int listId)
        {
            var tasks = listId == 0
                ? await _context.Tasks.ToListAsync()
                : await _context.Tasks.Where(t => t.ListId == listId).ToListAsync();

            return Json(tasks);
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
        public IActionResult AllTasks()
        {
            // Get the user id from the session
            int? userId = HttpContext.Session.GetInt32("UserId");

            if (userId.HasValue)
            {
                // Retrieve all tasks associated with the user id
                var allTasks = _context.Tasks.Where(t => t.UserId == userId.Value).ToList();
                return PartialView("_ListTasksPartial", allTasks);
            }
            else
            {
                // Handle the case where the user id is not available
                return RedirectToAction("Login", "Account");
            }
        }
        [HttpGet]
        public IActionResult TodayTasks()
        {
            // Get the user id from the session
            int? userId = HttpContext.Session.GetInt32("UserId");

            if (userId.HasValue)
            {
                // Retrieve today's tasks associated with the user id
                var todayTasks = _context.Tasks.Where(t => t.UserId == userId.Value && t.TaskDate == DateTime.Today).ToList();
                return PartialView("_ListTasksPartial", todayTasks);
            }
            else
            {
                // Handle the case where the user id is not available
                return RedirectToAction("Login", "Account");
            }
        }
        [HttpGet]
        public IActionResult TomorrowTasks()
        {
            // Get the user id from the session
            int? userId = HttpContext.Session.GetInt32("UserId");

            if (userId.HasValue)
            {
                // Retrieve tomorrow's tasks associated with the user id
                var tomorrowTasks = _context.Tasks.Where(t => t.UserId == userId.Value && t.TaskDate == DateTime.Today.AddDays(1)).ToList();
                return PartialView("_ListTasksPartial", tomorrowTasks);
            }
            else
            {
                // Handle the case where the user id is not available
                return RedirectToAction("Login", "Account");
            }
        }
        [HttpGet]
        public IActionResult ListTasks(int id)
        {
            // Get the user id from the session
            int? userId = HttpContext.Session.GetInt32("UserId");

            if (userId.HasValue)
            {
                // Retrieve tasks associated with the list id and user id
                var list = _context.Lists
                    .Include(l => l.Tasks)
                    .FirstOrDefault(l => l.ListId == id && l.UserId == userId.Value);

                if (list == null)
                {
                    return NotFound();
                }

                return PartialView("_ListTasksPartial", list.Tasks);
            }
            else
            {
                // Handle the case where the user id is not available
                return RedirectToAction("Login", "Account");
            }
        }

        [HttpGet]
        public IActionResult TaskDetails(int id)
        {
            var task = _context.Tasks.FirstOrDefault(t => t.TaskId == id);

            if (task == null)
            {
                return NotFound();
            }

            return PartialView("_TaskDetailsPartial", task);
        }
        // Implement other actions similarly...

        [HttpPost]
        public IActionResult CreateList(List newList)
        {
            // Get the user id from the session
            int? userId = HttpContext.Session.GetInt32("UserId");

            if (userId.HasValue)
            {
                // Assign the user id to the list
                newList.UserId = userId.Value;

                // Add the list to the database
                _context.Lists.Add(newList);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            else
            {
                // Handle the case where the user id is not available
                return RedirectToAction("Login", "Account");
            }
        }

        [HttpPost]
        public IActionResult CreateTask(int listId, Task newTask)
        {
            // Get the user id from the session
            int? userId = HttpContext.Session.GetInt32("UserId");

            if (userId.HasValue)
            {
                // Assign the user id to the task
                newTask.UserId = userId.Value;

                if (listId == -1)
                {
                    // For listId = -1, do not assign a list to the task
                }
                else if (listId == -2)
                {
                    // For listId = -2, assign the date to today
                    newTask.TaskDate = DateTime.Today;
                }
                else if (listId == -3)
                {
                    // For listId = -3, assign the date to tomorrow
                    newTask.TaskDate = DateTime.Today.AddDays(1);
                }
                else
                {
                    // For other listIds, assign the list to the task
                    var list = _context.Lists.FirstOrDefault(l => l.ListId == listId);
                    if (list == null)
                    {
                        return NotFound();
                    }
                    newTask.ListId = listId;
                }

                if (newTask.TaskStartTime.HasValue && newTask.TaskEndTime.HasValue)
                {
                    newTask.TaskDuration = newTask.TaskEndTime - newTask.TaskStartTime;
                }

                // Add the task to the database
                _context.Tasks.Add(newTask);
                _context.SaveChanges(); // Ensure changes are saved to the database
                return RedirectToAction("Index");
            }
            else
            {
                // Handle the case where the user id is not available
                return RedirectToAction("Login", "Account");
            }
        }
        [HttpPost]
        public IActionResult DeleteTask(int id)
        {
            var task = _context.Tasks.FirstOrDefault(t => t.TaskId == id);
            if (task == null)
            {
                return NotFound();
            }

            _context.Tasks.Remove(task);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
        public IActionResult GetTaskDetails(int id)
        {
            var task = _context.Tasks.FirstOrDefault(t => t.TaskId == id);
            if (task == null)
            {
                return NotFound();
            }

            return Json(task); // Return JSON data containing task details
        }

        public IActionResult GetListDetails(int id)
        {
            var list = _context.Lists.FirstOrDefault(l => l.ListId == id);
            if (list == null)
            {
                return NotFound();
            }

            return Json(list); // Return JSON data containing list details
        }
        [HttpPost]
        public IActionResult EditList(int listId, List updatedList)
        {
            var list = _context.Lists.FirstOrDefault(l => l.ListId == listId);
            if (list == null)
            {
                return NotFound();
            }

            // Update list properties
            list.ListTitle = updatedList.ListTitle;
            list.ListCategory = updatedList.ListCategory;

            _context.SaveChanges();
            return Json(new { success = true }); // Return success message
        }
        [HttpPost]
        public IActionResult EditTask(int taskId, Task updatedTask)
        {
            var task = _context.Tasks.FirstOrDefault(t => t.TaskId == taskId);
            if (task == null)
            {
                return NotFound();
            }

            // Update task properties
            task.TaskTitle = updatedTask.TaskTitle;
            task.TaskPriority = updatedTask.TaskPriority;
            // Update other task properties similarly

            _context.SaveChanges();
            return Json(new { success = true }); // Return success message
        }

        // Implement other actions similarly...
        [HttpPost]
        public IActionResult UpdateTaskStatus(int id, bool isChecked)
        {
            var task = _context.Tasks.FirstOrDefault(t => t.TaskId == id);
            if (task == null)
            {
                return NotFound();
            }

            task.TaskStatus = isChecked;
            _context.SaveChanges();

            return Json(new { success = true });
        }

    }
}
