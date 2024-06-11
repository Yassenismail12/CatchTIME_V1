using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using TESTT.Models;

namespace TESTT.Controllers
{
    [Authorize]
    public class ProjectController : Controller
    {
        private readonly CatchTIMEContext _context;

        public ProjectController(CatchTIMEContext context)
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

        private async Task<List<Project>> GetProjectsByUserIdAsync(int userId)
        {
            return await _context.Projects
                .Include(p => p.Tasks)
                .Where(p => p.UserId == userId)
                .ToListAsync();
        }

        private async Task<Project> GetProjectByIdAndUserIdAsync(int id, int userId)
        {
            return await _context.Projects
                .Include(p => p.Tasks)
                .FirstOrDefaultAsync(p => p.ProjectId == id && p.UserId == userId);
        }

        // GET: Project
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var userId = GetUserIdFromSession();
            if (!userId.HasValue)
            {
                return HandleUnauthorizedAccess();
            }

            var projects = await GetProjectsByUserIdAsync(userId.Value);

            foreach (var project in projects)
            {
                project.ProjectNoOfTasks = project.Tasks.Count();
                project.ProjectNoOfCompleted = project.Tasks.Count(t => t.TaskStatus);
            }

            await _context.SaveChangesAsync();

            return View(projects);
        }

        // GET: Project/Details/1
        [HttpGet]
        public async Task<IActionResult> Details(int? id)
        {
            var userId = GetUserIdFromSession();
            if (!userId.HasValue || id == null)
            {
                return HandleUnauthorizedAccess();
            }

            var project = await GetProjectByIdAndUserIdAsync(id.Value, userId.Value);

            if (project == null)
            {
                return NotFound();
            }

            return View(project);
        }

        // GET: Project/Create
        [HttpGet]
        public IActionResult Create()
        {
            var userId = GetUserIdFromSession();
            if (!userId.HasValue)
            {
                return HandleUnauthorizedAccess();
            }

            ViewBag.Lists = new SelectList(_context.Lists.Where(l => l.UserId == userId), "ListId", "ListTitle");
            return View();
        }

        // POST: Project/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ProjectTitle,ProjectNoOfTasks,ListId")] Project project)
        {
            var userId = GetUserIdFromSession();
            if (!userId.HasValue)
            {
                return HandleUnauthorizedAccess();
            }

            project.UserId = userId.Value;

            if (ModelState.IsValid)
            {
                _context.Add(project);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            ViewBag.Lists = new SelectList(_context.Lists.Where(l => l.UserId == userId), "ListId", "ListTitle", project.ListId);
            return View(project);
        }

        // GET: Project/Edit/1
        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            var userId = GetUserIdFromSession();
            if (!userId.HasValue || id == null)
            {
                return HandleUnauthorizedAccess();
            }

            var project = await GetProjectByIdAndUserIdAsync(id.Value, userId.Value);
            if (project == null)
            {
                return NotFound();
            }

            ViewBag.Lists = new SelectList(_context.Lists.Where(l => l.UserId == userId), "ListId", "ListTitle", project.ListId);
            return View(project);
        }

        // POST: Project/Edit/1
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ProjectId,ProjectTitle,ProjectNoOfTasks,ListId")] Project project)
        {
            var userId = GetUserIdFromSession();
            if (!userId.HasValue || id != project.ProjectId)
            {
                return HandleUnauthorizedAccess();
            }

            project.UserId = userId.Value;

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(project);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProjectExists(project.ProjectId, userId.Value))
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

            ViewBag.Lists = new SelectList(_context.Lists.Where(l => l.UserId == userId), "ListId", "ListTitle", project.ListId);
            return View(project);
        }

        // GET: Project/Delete/1
        [HttpGet]
        public async Task<IActionResult> Delete(int? id)
        {
            var userId = GetUserIdFromSession();
            if (!userId.HasValue || id == null)
            {
                return HandleUnauthorizedAccess();
            }

            var project = await GetProjectByIdAndUserIdAsync(id.Value, userId.Value);
            if (project == null)
            {
                return NotFound();
            }

            return View(project);
        }

        // POST: Project/Delete/1
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var userId = GetUserIdFromSession();
            if (!userId.HasValue)
            {
                return HandleUnauthorizedAccess();
            }

            var project = await GetProjectByIdAndUserIdAsync(id, userId.Value);
            if (project == null)
            {
                return NotFound();
            }

            _context.Projects.Remove(project);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProjectExists(int id, int userId)
        {
            return _context.Projects.Any(e => e.ProjectId == id && e.UserId == userId);
        }
    }
}
