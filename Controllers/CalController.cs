using Microsoft.AspNetCore.Mvc;

namespace TESTT.Controllers
{
    public class CalController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
