// ProjectController.cs

using Microsoft.AspNetCore.Mvc;
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
        var projects = await _context.Projects.ToListAsync();
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

        return View(project);
    }

    // GET: Project/Create
    public IActionResult Create()
    {
        return View();
    }

    // POST: Project/Create
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("ProjectTitle")] Project project)
    {
        if (ModelState.IsValid)
        {
            _context.Add(project);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        return View(project);
    }
}
