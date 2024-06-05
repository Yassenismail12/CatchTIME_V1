using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TESTT.Models;
using System;
using System.Linq;
using System.Threading.Tasks;
using Task = TESTT.Models.Task;

public class TaskController : Controller
{
    private readonly CatchTIMEContext _context;

    public TaskController(CatchTIMEContext context)
    {
        _context = context;
    }

    // GET: Task
    public async Task<IActionResult> Index(string list)
    {
        IQueryable<Task> tasks = _context.Tasks;

        switch (list)
        {
            case "Today":
                tasks = tasks.Where(t => t.TaskDate == DateTime.Today);
                break;
            case "Tomorrow":
                tasks = tasks.Where(t => t.TaskDate == DateTime.Today.AddDays(1));
                break;
            case "All":
                // All tasks, no filter needed
                break;
            default:
                // Default to today's tasks if no list is specified
                tasks = tasks.Where(t => t.TaskDate == DateTime.Today);
                break;
        }

        var taskList = await tasks.ToListAsync();
        ViewBag.List = list;
        return View(taskList);
    }

    // GET: Task/Create
    public IActionResult Create()
    {
        return View();
    }

    // POST: Task/Create
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("TaskTitle,TaskPriority,TaskTag,TaskDate,TaskDuration,TaskStartTime,TaskEndTime,TaskActualDuration,TaskDifficulty,TaskStatus")] TESTT.Models.Task task)
    {
        if (ModelState.IsValid)
        {
            if (task.TaskStartTime.HasValue && task.TaskEndTime.HasValue)
            {
                task.TaskDuration = task.TaskEndTime - task.TaskStartTime;
            }

            _context.Add(task);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        return View(task);
    }

    // GET: Task/Edit/1
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

        return View(task);
    }

    // POST: Task/Edit/1
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, [Bind("TaskId,TaskTitle,TaskPriority,TaskTag,TaskDate,TaskDuration,TaskStartTime,TaskEndTime,TaskActualDuration,TaskDifficulty,TaskStatus")] TESTT.Models.Task task)
    {
        if (id != task.TaskId)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            if (task.TaskStartTime.HasValue && task.TaskEndTime.HasValue)
            {
                task.TaskDuration = task.TaskEndTime - task.TaskStartTime;
            }

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

        return View(task);
    }

    // GET: Task/Delete/1
    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var task = await _context.Tasks
            .FirstOrDefaultAsync(m => m.TaskId == id);
        if (task == null)
        {
            return NotFound();
        }

        return View(task);
    }

    // POST: Task/Delete/1
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var task = await _context.Tasks.FindAsync(id);
        _context.Tasks.Remove(task);
        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    // GET: Task/Details/1
    public async Task<IActionResult> Details(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var task = await _context.Tasks
            .FirstOrDefaultAsync(m => m.TaskId == id);
        if (task == null)
        {
            return NotFound();
        }

        return View(task);
    }

    private bool TaskExists(int id)
    {
        return _context.Tasks.Any(e => e.TaskId == id);
    }
}
