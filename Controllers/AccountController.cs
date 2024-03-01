using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TESTT.Models;
using System.Security.Cryptography;
using System.Linq;
using System.Text;
using BCrypt.Net;
using Microsoft.AspNetCore.Authorization;


namespace TESTT.Controllers
{
    public class AccountController : Controller
    {
        private readonly CatchTIMEContext _context;

        // Constructor with dependency injection
        public AccountController(CatchTIMEContext context)
        {
            _context = context;
        }

        // GET: AccountController
        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Login(string email, string password)
        {
            var user = _context.UserTables.SingleOrDefault(u => u.UserEmail == email && u.UserPassword == password);

            if (user != null)
            {
                // Authentication successful, redirect to the Dashboard
                HttpContext.Session.SetInt32("UserId", user.UserId);
                return RedirectToAction("Index", "TimeManagement");
            }
            else
            {
                // Authentication failed, display an error message
                ViewBag.InvalidLoginMessage = "Invalid Email or password.";
                return View();
            }
        }


        private string HashPassword(string password)
        {
            return BCrypt.Net.BCrypt.HashPassword(password);
        }

        // Verify password using Bcrypt
        private bool VerifyPassword(string inputPassword, string hashedPassword)
        {
            return BCrypt.Net.BCrypt.Verify(inputPassword, hashedPassword);
        }

        // GET: AccountController/Create
        [HttpGet]
        public IActionResult Create()
        {
            var model = new CreateAccountViewModel();
            return View(model);
        }


        // POST: AccountController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(CreateAccountViewModel model)
        {
            if (ModelState.IsValid)
            {
                // Check if the email is already in use
                if (_context.UserTables.Any(u => u.UserEmail == model.UserEmail))
                {
                    ModelState.AddModelError("UserEmail", "Email is already in use.");
                    return View(model);
                }

                // Hash the password before storing it
                model.UserPassword = HashPassword(model.UserPassword);

                // Perform additional validation and logic as needed

                // Create a new UserTable instance and populate it with the ViewModel data
                var newUser = new UserTable
                {
                    Username = model.Name,
                    UserEmail = model.UserEmail,
                    UserPassword = model.UserPassword
                    // Add other properties as needed
                };

                // Add the user to the database
                _context.UserTables.Add(newUser);
                _context.SaveChanges();

                // Redirect to the Dashboard or any other page after successful registration
                HttpContext.Session.SetInt32("UserId", newUser.UserId);
                return RedirectToAction("Index", "TimeManagement");
            }

            // If the model is not valid, redisplay the form with validation errors
            return View(model);
        }


        // GET: AccountController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: AccountController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: AccountController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: AccountController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
