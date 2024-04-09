using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TESTT.Models;

namespace TESTT.Controllers
{
    public class PomodoroController : Controller
    {
        private readonly CatchTIMEContext _context;

        public PomodoroController(CatchTIMEContext context)
        {
            _context = context;
        }

        // GET: Pomodoro
        public async Task<IActionResult> Index()
        {
            var tasks = await _context.Tasks.ToListAsync();
            ViewBag.Tasks = tasks; // Pass the list of tasks to the view
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> StartPomodoro(int taskId)
        {
            var task = await _context.Tasks.FindAsync(taskId);
            if (task == null)
            {
                return NotFound();
            }

            // Check if Pomodoro is already running
            if (task.TaskStartTime != null)
            {
                return BadRequest("Pomodoro timer is already running.");
            }

            // Set Pomodoro start time to current time
            task.TaskStartTime = DateTime.Now.TimeOfDay;
            await _context.SaveChangesAsync();

            return Json(new { success = true });
        }

        [HttpPost]
        public async Task<IActionResult> StopPomodoro(int taskId)
        {
            var task = await _context.Tasks.FindAsync(taskId);
            if (task == null)
            {
                return NotFound();
            }

            // Check if Pomodoro is not started
            if (task.TaskStartTime == null)
            {
                return BadRequest("Pomodoro timer has not started.");
            }

            // Calculate Pomodoro duration based on start time
            var currentTime = DateTime.Now.TimeOfDay;
            var startTime = task.TaskStartTime.Value;
            var pomodoroDuration = currentTime - startTime;

            // Ensure the Pomodoro duration is positive
            if (pomodoroDuration.TotalMilliseconds < 0)
            {
                return BadRequest("Invalid Pomodoro duration.");
            }

            // Update task's actual duration with Pomodoro duration
            task.TaskActualDuration = task.TaskActualDuration.HasValue
                ? task.TaskActualDuration + pomodoroDuration
                : pomodoroDuration;

            // Reset TaskStartTime to null to indicate the end of the Pomodoro
            task.TaskStartTime = null;

            // Save changes to the database
            await _context.SaveChangesAsync();

            return Json(new { success = true });
        }
    }
}
