using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using TESTT.Models;

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
