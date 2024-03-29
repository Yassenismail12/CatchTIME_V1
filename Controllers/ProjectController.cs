// ProjectController.cs

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using TESTT.Models;

public class ProjectController : Controller
{
    private readonly CatchTIMEContext _context;

    public ProjectController(CatchTIMEContext context)
    {
        _context = context;
    }

    // GET: Project
    public async Task<IActionResult> Index()
    {
        var projects = await _context.Projects.Include(p => p.Tasks).ToListAsync();

        foreach (var project in projects)
        {
            project.ProjectNoOfTasks = project.Tasks.Count();
            project.ProjectNoOfCompleted = project.Tasks.Count(t => t.TaskStatus == true);
            
        }

        await _context.SaveChangesAsync();

        return View(projects);
    }

    // GET: Project/Details/1
    public async Task<IActionResult> Details(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var project = await _context.Projects
            .Include(p => p.Tasks)
            .FirstOrDefaultAsync(m => m.ProjectId == id);

        if (project == null)
        {
            return NotFound();
        }

        project.ProjectNoOfTasks = project.Tasks.Count();
        project.ProjectNoOfCompleted = project.Tasks.Count(t => t.TaskStatus == true);
        await _context.SaveChangesAsync();

        return View(project);
    }

    // GET: Project/Create
    public IActionResult Create()
    {
        ViewBag.Lists = new SelectList(_context.Lists, "ListId", "ListTitle");
        return View();
    }

    // POST: Project/Create
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("ProjectTitle, ListId")] Project project)
    {
        if (ModelState.IsValid)
        {
            _context.Add(project);
            await _context.SaveChangesAsync();

            // Update tasks of the project to belong to the selected list
            var tasksToUpdate = await _context.Tasks.Where(t => t.ProjectId == project.ProjectId).ToListAsync();
            foreach (var task in tasksToUpdate)
            {
                task.ListId = project.ListId;
            }
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }
        return View(project);
    }

    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var project = await _context.Projects.FindAsync(id);
        if (project == null)
        {
            return NotFound();
        }

        ViewBag.Lists = new SelectList(_context.Lists, "ListId", "ListTitle", project.ListId);
        return View(project);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, [Bind("ProjectId, ProjectTitle, ListId")] Project project)
    {
        if (id != project.ProjectId)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            try
            {
                _context.Update(project);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProjectExists(project.ProjectId))
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
        ViewBag.Lists = new SelectList(_context.Lists, "ListId", "ListTitle", project.ListId);
        return View(project);
    }

    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var project = await _context.Projects
            .FirstOrDefaultAsync(m => m.ProjectId == id);
        if (project == null)
        {
            return NotFound();
        }

        return View(project);
    }

    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var project = await _context.Projects.FindAsync(id);
        _context.Projects.Remove(project);
        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    private bool ProjectExists(int id)
    {
        return _context.Projects.Any(e => e.ProjectId == id);
    }
}
