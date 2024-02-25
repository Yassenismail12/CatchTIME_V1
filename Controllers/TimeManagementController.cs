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
    public class TimeManagementController : Controller
    {
        private readonly CatchTIMEContext _context;

        public TimeManagementController(CatchTIMEContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var lists = _context.Lists.ToList();
            var allTasks = _context.Tasks.ToList();
            var todayTasks = _context.Tasks.Where(t => t.TaskDate == DateTime.Today).ToList();
            var tomorrowTasks = _context.Tasks.Where(t => t.TaskDate == DateTime.Today.AddDays(1)).ToList();

            var viewModel = new TimeManagementViewModel
            {
                AllLists = lists,
                AllTasks = allTasks,
                TodayTasks = todayTasks,
                TomorrowTasks = tomorrowTasks,
                AllListId = -1, // Add this property to ViewModel and set it to -1 for All list
                TodayListId = -2, // Add this property to ViewModel and set it to -2 for Today list
                TomorrowListId = -3 // Add this property to ViewModel and set it to -3 for Tomorrow list
            };

            return View(viewModel);
        }

        public IActionResult AllTasks()
        {
            var allTasks = _context.Tasks.ToList();
            return PartialView("_ListTasksPartial", allTasks);
        }

        public IActionResult TodayTasks()
        {
            var todayTasks = _context.Tasks.Where(t => t.TaskDate == DateTime.Today).ToList();
            return PartialView("_ListTasksPartial", todayTasks);
        }

        public IActionResult TomorrowTasks()
        {
            var tomorrowTasks = _context.Tasks.Where(t => t.TaskDate == DateTime.Today.AddDays(1)).ToList();
            return PartialView("_ListTasksPartial", tomorrowTasks);
        }

        public IActionResult ListTasks(int id)
        {
            var list = _context.Lists
                .Include(l => l.Tasks)
                .FirstOrDefault(l => l.ListId == id);

            if (list == null)
            {
                return NotFound();
            }

            return PartialView("_ListTasksPartial", list.Tasks);
        }

        public IActionResult TaskDetails(int id)
        {
            var task = _context.Tasks.FirstOrDefault(t => t.TaskId == id);

            if (task == null)
            {
                return NotFound();
            }

            return PartialView("_TaskDetailsPartial", task);
        }
        public IActionResult CreateList(List newList)
        {
            _context.Lists.Add(newList);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult CreateTask(int listId, Task newTask)
        {
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

            _context.Tasks.Add(newTask);
            _context.SaveChanges(); // Ensure changes are saved to the database
            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult DeleteList(int id)
        {
            var list = _context.Lists.FirstOrDefault(l => l.ListId == id);
            if (list == null)
            {
                return NotFound();
            }

            _context.Lists.Remove(list);
            _context.SaveChanges();
            return RedirectToAction("Index");
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
