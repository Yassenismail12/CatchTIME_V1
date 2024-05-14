using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using TESTT.Models;

namespace TESTT.Controllers
{
    public class DashboardController : Controller
    {
        private readonly CatchTIMEContext _context;

        public DashboardController(CatchTIMEContext context)
        {
            _context = context;
        }
		// Action for the main view
		public IActionResult Index()
		{
			// Fetch data from the database
			// Get the user id from the session
			int? userId = HttpContext.Session.GetInt32("UserId");

			if (userId.HasValue)
			{
				// Completed tasks count
				int completedTasksCount = _context.Tasks.Count(t => t.UserId == userId && t.TaskStatus == true);

				// Uncompleted tasks count
				int uncompletedTasksCount = _context.Tasks.Count(t => t.UserId == userId && t.TaskStatus == false);

				// Total tasks count
				int totalTasksCount = _context.Tasks.Count(t => t.UserId == userId);
				// Fetch todo list for the user
				var todoList = _context.Tasks
					.Where(t => t.UserId == userId)
					.OrderBy(t => t.TaskDate)
					.Take(5)
					.Select(t => new
					{
						Title = t.TaskTitle,
						Status = t.TaskStatus ? "completed" : "not-completed"
					})
					.ToList();

				// Fetch user data
				var user = _context.UserTables.FirstOrDefault(u => u.UserId == userId);

				// Pass the data to the view
				ViewBag.CompletedTasksCount = completedTasksCount;
				ViewBag.UncompletedTasksCount = uncompletedTasksCount;
				ViewBag.TotalTasksCount = totalTasksCount;
				ViewBag.TodoList = todoList;
				ViewBag.User = user;

				// Return the view with the data
				return View();
			}
			else
			{
				// Handle the case where the user id is not available
				return RedirectToAction("Login", "Account");
			}
		}

	}

	// Define a view model to hold the data for the view
	public class AdminHubViewModel
    {
        public int CompletedTasksCount { get; set; }
        public int UncompletedTasksCount { get; set; }
        public int TotalTasksCount { get; set; }
        public List<object> RecentOrders { get; set; }
        public List<object> TodoList { get; set; }
    }
}
