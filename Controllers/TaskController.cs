using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TESTT.Models;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace TESTT.Controllers
{
    public class TaskController : Controller
    {
        private readonly CatchTIMEContext _context;

        public TaskController(CatchTIMEContext context)
        {
            _context = context;
        }

        // GET: Tasks
        public async Task<IActionResult> Index()
        {
            var tasks = await _context.Tasks.Include(t => t.List).ToListAsync();
            var lists = await _context.Lists.ToListAsync();
            return View(new TasksViewModel { Tasks = tasks, Lists = lists });
        }

        // GET: Tasks/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var task = await _context.Tasks
                .Include(t => t.List)
                .FirstOrDefaultAsync(m => m.TaskId == id);
            if (task == null)
            {
                return NotFound();
            }

            return View(task);
        }

        // GET: Tasks/Create
        public IActionResult Create()
        {
            ViewData["ListId"] = new SelectList(_context.Lists, "ListId", "ListTitle");
            return View();
        }

        // POST: Tasks/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("TaskTitle,TaskPriority,TaskTag,TaskDate,TaskDuration,TaskStartTime,TaskEndTime,TaskActualDuration,TaskDifficulty,TaskStatus,ListId")] TESTT.Models.Task task)
        {
            if (ModelState.IsValid)
            {
                _context.Add(task);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ListId"] = new SelectList(_context.Lists, "ListId", "ListTitle", task.ListId);
            return View(task);
        }

        // GET: Tasks/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var task = await _context.Tasks.FindAsync(id);
            if (task == null)
            {
                return NotFound();
            }
            ViewData["ListId"] = new SelectList(_context.Lists, "ListId", "ListTitle", task.ListId);
            return View(task);
        }

        // POST: Tasks/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("TaskId,TaskTitle,TaskPriority,TaskTag,TaskDate,TaskDuration,TaskStartTime,TaskEndTime,TaskActualDuration,TaskDifficulty,TaskStatus,ListId")] TESTT.Models.Task task)
        {
            if (id != task.TaskId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(task);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TaskExists(task.TaskId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["ListId"] = new SelectList(_context.Lists, "ListId", "ListTitle", task.ListId);
            return View(task);
        }

        // GET: Tasks/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var task = await _context.Tasks
                .Include(t => t.List)
                .FirstOrDefaultAsync(m => m.TaskId == id);
            if (task == null)
            {
                return NotFound();
            }

            return View(task);
        }

        // POST: Tasks/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var task = await _context.Tasks.FindAsync(id);
            _context.Tasks.Remove(task);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TaskExists(int id)
        {
            return _context.Tasks.Any(e => e.TaskId == id);
        }

        [HttpGet]
        public async Task<IActionResult> GetTasksByList(int listId)
        {
            var tasks = await _context.Tasks.Where(t => t.ListId == listId).ToListAsync();
            return Json(tasks);
        }
    }
}
