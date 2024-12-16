using Microsoft.AspNetCore.Mvc;
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
        public IActionResult Login()
        {
            return View();
        }

        public IActionResult Register()
        {
            return View();
        }

        public IActionResult ForgotPassword()
        {
            return View();
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}



