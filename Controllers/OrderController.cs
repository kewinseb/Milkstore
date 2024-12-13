using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MilkStore.Data;

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
            // Fetch all products from the database
            var orders = _context.Orders.ToList();

            return View(orders);
        }
    }
}
