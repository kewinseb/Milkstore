using Microsoft.AspNetCore.Mvc;
using MilkStore.Data;
using MilkStore.Models;
using System.Linq;

namespace MilkStore.Controllers
{
    public class OrderController : Controller
    {
        private readonly MilkstoreDbContext _context;

        public OrderController(MilkstoreDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var orders = _context.Orders.ToList(); // Fetch all orders from the database
            return View(orders); // Pass the data to the view
        }
    }
}