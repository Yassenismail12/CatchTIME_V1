using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TESTT.Models;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;

public class TaskController : Controller
{
    private readonly CatchTIMEContext _context;

    public TaskController(CatchTIMEContext context)
    {
        _context = context;
    }

    // GET: Task
    public async Task<IActionResult> Index()
    {
        var tasks = await _context.Tasks.ToListAsync();
        return View(tasks);
    }

    // GET: Task/Create
    public IActionResult Create()
    {
        ViewBag.Projects = new SelectList(_context.Projects, "ProjectId", "ProjectTitle");
        ViewBag.Lists = new SelectList(_context.Lists, "ListId", "ListTitle");

        return View();
    }



    // POST: Task/Create
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("TaskTitle,TaskPriority,TaskTag,TaskDate,TaskDuration,TaskStartTime,TaskEndTime,TaskActualDuration,TaskDifficulty,TaskStatus,ProjectId,ListId")] TESTT.Models.Task task)
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

        // If the model is not valid, we need to set up the ViewBag again for the dropdowns
        ViewBag.Projects = new SelectList(_context.Projects, "ProjectId", "ProjectTitle", task.ProjectId);
        ViewBag.Lists = new SelectList(_context.Lists, "ListId", "ListTitle", task.ListId);
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

        // Get the projects and lists from the database
        var projects = await _context.Projects.ToListAsync();
        var lists = await _context.Lists.ToListAsync();

        // Create SelectList for projects and lists
        SelectList projectList = new SelectList(projects, "ProjectId", "ProjectTitle", task.ProjectId);
        SelectList listList = new SelectList(lists, "ListId", "ListTitle", task.ListId);

        // Pass SelectList to the view
        ViewData["Projects"] = projectList;
        ViewData["Lists"] = listList;

        return View(task);
    }

    // POST: Task/Edit/1
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, [Bind("TaskId,TaskTitle,TaskPriority,TaskTag,TaskDate,TaskDuration,TaskStartTime,TaskEndTime,TaskActualDuration,TaskDifficulty,TaskStatus,ProjectId,ListId")] TESTT.Models.Task task)
    {
        if (id != task.TaskId)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            // Calculate TaskDuration based on TaskStartTime and TaskEndTime
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

        // If the model is not valid, set up ViewData for the dropdowns
        ViewData["Projects"] = new SelectList(_context.Projects, "ProjectId", "ProjectTitle", task.ProjectId);
        ViewData["Lists"] = new SelectList(_context.Lists, "ListId", "ListTitle", task.ListId);

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

    [HttpPost]
    public async Task<IActionResult> UpdateStatus(int taskId)
    {
        var task = await _context.Tasks.FindAsync(taskId);
        if (task != null)
        {
            task.TaskStatus = !task.TaskStatus;
            await _context.SaveChangesAsync();
            return Json(new { success = true });
        }

        return Json(new { success = false, error = "Task not found" });
    }

    // GET: Task/Details/1
    public async Task<IActionResult> Details(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var task = await _context.Tasks
            .Include(t => t.List)
            .Include(t => t.Project)
            .Include(t => t.User)
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

    public IActionResult GetTasks()
    {
        var tasks = _context.Tasks.Select(t => new
        {
            id = t.TaskId,
            title = t.TaskTitle,
            start = (t.TaskDate != null && t.TaskStartTime != null) ? t.TaskDate.Value.ToString("yyyy-MM-dd") + "T" + t.TaskStartTime.Value.ToString() : null,
            end = (t.TaskDate != null && t.TaskEndTime != null) ? t.TaskDate.Value.ToString("yyyy-MM-dd") + "T" + t.TaskEndTime.Value.ToString() : null,
            // Add more properties as needed
        }).ToList();

        return Json(tasks);
    }







}
