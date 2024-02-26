using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using TESTT.Models;
using TaskModel = TESTT.Models.Task;

public class ListController : Controller
{
    private readonly CatchTIMEContext _context;

    public ListController(CatchTIMEContext context)
    {
        _context = context;
    }

    // GET: List
    public async Task<IActionResult> Index()
    {
        var lists = await _context.Lists.ToListAsync();
        return View(lists);
    }

    // GET: List/Details/1
    public async Task<IActionResult> Details(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var list = await _context.Lists
            .Include(l => l.Tasks)
            .FirstOrDefaultAsync(m => m.ListId == id);

        if (list == null)
        {
            return NotFound();
        }

        return View(list);
    }
    private ListDetailsViewModel GetListDetailsFromDataSource(int listId)
    {
        // Example: Fetch details and tasks for a list from a data source
        // Replace this with actual logic to retrieve data from your database or other source

        // Assuming you have a DbSet<TaskModel> in your DbContext
        var listDetails = _context.Lists
            .Where(l => l.ListId == listId)
            .Select(l => new ListDetailsViewModel
            {
                ListTitle = l.ListTitle,
                ListCategory = l.ListCategory,
                Tasks = _context.Tasks
                    .Where(t => t.ListId == listId)
                    .ToList()
            })
            .FirstOrDefault();

        return listDetails;
    }
    [HttpGet]
    public IActionResult GetListDetails(int id)
    {
        // Assuming you have a method to fetch details and tasks for a list from your data source
        var listDetails = GetListDetailsFromDataSource(id);

        return PartialView("_ListDetailsPartial", listDetails);
    }
    // GET: List/Create
    public IActionResult Create()
    {
        return View();
    }

    // POST: List/Create
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("ListTitle,ListCategory")] List list)
    {
        if (ModelState.IsValid)
        {
            _context.Add(list);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        return View(list);
    }
}
