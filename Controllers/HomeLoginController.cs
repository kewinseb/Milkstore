using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MilkStore.Data;
using MilkStore.Models;
using System.Diagnostics;

namespace MilkStore.Controllers
{
    public class HomeLoginController : Controller
    {
        private readonly MilkstoreDbContext _context;

        public HomeLoginController(MilkstoreDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(string emailId, string password)
        {
            // Find user with the provided email and password
            var user = _context.Users
                               .FirstOrDefault(u => u.EmailId == emailId && u.Password == password);

            if (user != null)
            {
                HttpContext.Session.SetString("UserEmailId", user.EmailId);

                // Redirect to Home page on successful login
                return RedirectToAction("Index", "Home");
            }

            // Return error message if login fails
            ViewBag.ErrorMessage = "Invalid Email or Password.";
            return View();
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Register(User model)
        {
            // Check if the email is already registered
            if (_context.Users.Any(u => u.EmailId == model.EmailId))
            {
                // Add an error to the ModelState
                ModelState.AddModelError("EmailId", "This email is already registered.");
            }

            // Proceed if the model state is valid
            if (ModelState.IsValid)
            {
                // Create a new user object and set default role
                var user = new User
                {
                    Name = model.Name,
                    EmailId = model.EmailId,
                    Password = model.Password,
                    Address = model.Address,
                    City = model.City,
                    State = model.State,
                    PinCode = model.PinCode,
                    Phone = model.Phone,
                    Role = "user", // Automatically set the role to "user"
                    CreatedAt = DateTime.Now,
                    UpdatedAt = DateTime.Now
                };

                // Save the user to the database
                _context.Users.Add(user);
                _context.SaveChanges();

                // Redirect to the login page or success page
                return RedirectToAction("Login");
            }

            // If model state is invalid, reload the registration page with the existing data
            return View(model);
        }

        [HttpGet]
        public IActionResult ForgotPassword()
        {
            return View();
        }

        [HttpPost]
        public IActionResult ForgotPassword(string emailId, string password, string confirmPassword)
        {
            // Check if the email exists in the Users table
            var user = _context.Users.FirstOrDefault(u => u.EmailId == emailId);
            if (user == null)
            {
                ModelState.AddModelError("EmailId", "Email ID does not exist.");
                return View(); // Return the view with the error
            }

            // Check if password and confirm password match
            if (password.Trim() != confirmPassword.Trim())
            {
                ModelState.AddModelError("Password", "Password and Confirm Password do not match.");
                return View();
            }

            // Update the password for the user
            user.Password = password;
            user.UpdatedAt = DateTime.Now;
            _context.SaveChanges();

            return RedirectToAction("Login");
        }

        public IActionResult Logout()
        {
            return View();
        }
    }
}
   


