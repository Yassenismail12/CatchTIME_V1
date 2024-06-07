using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace YourNamespace.Controllers
{
    public class InsightsController : Controller
    {
        // GET: Insights
        public IActionResult Index()
        {
            // Here, you can fetch data from your database or service to pass to the view.
            var completedTasks = new List<int> { 50, 60, 70, 80, 90, 100, 110, 120, 130, 140, 150, 160 }; // Example data
            var productivityData = new List<int> { 65, 59, 80, 81, 56, 55, 40 }; // Example data

            // Create a ViewModel to hold your data (if needed)
            var model = new InsightsViewModel
            {
                CompletedTasks = completedTasks,
                ProductivityData = productivityData,
                Recommendations = new List<string>
                {
                    "Take regular breaks to stay productive.",
                    "Use a task management tool to stay organized.",
                    "Set realistic goals and deadlines.",
                    "Review and reflect on your progress regularly.",
                    "Stay positive and motivated."
                }
            };

            return View(model);
        }
    }

    // Define a ViewModel to hold your data (optional but recommended)
    public class InsightsViewModel
    {
        public List<int> CompletedTasks { get; set; }
        public List<int> ProductivityData { get; set; }
        public List<string> Recommendations { get; set; }
    }
}
