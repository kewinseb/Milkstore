using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MilkStore.Data;

namespace YourNamespace.Controllers
{
    public class OrdersController : Controller
    {
        private readonly MilkstoreDbContext _context;

        public OrdersController(MilkstoreDbContext context)
        {
            _context = context;
        }

        // 1. Get All Orders
        public async Task<IActionResult> Index()
        {
            var orders = await _context.Orders.ToListAsync();
            return View(orders);
        }

        // 2. Get Orders by a Specific User
     /*   public async Task<IActionResult> OrdersByUser(string emailId)
        {
            var orders = await _context.Orders
                .Where(o => o.User_emailId == emailId)
                .ToListAsync();
            return View(orders);
        }*/

        // 3. Get Orders Within a Date Range
        public async Task<IActionResult> OrdersByDate(DateTime startDate, DateTime endDate)
        {
            var orders = await _context.Orders
                .Where(o => o.CreatedAt >= startDate && o.CreatedAt <= endDate)
                .ToListAsync();
            return View(orders);
        }

        // 4. Get Most Recent Orders
        public async Task<IActionResult> RecentOrders()
        {
            var orders = await _context.Orders
                .OrderByDescending(o => o.CreatedAt)
                .Take(10)
                .ToListAsync();
            return View(orders);
        }

        // 5. Get Total Sales Amount
        public async Task<IActionResult> TotalSales()
        {
            var totalSales = await _context.Orders.SumAsync(o => o.TotalAmount);
            ViewBag.TotalSales = totalSales;
            return View();
        }

        // 6. Get Order Count Per User
       /* public async Task<IActionResult> OrderCountPerUser()
        {
            var orderCounts = await _context.Orders
                .GroupBy(o => o.User_emailId)
                .Select(g => new { UserEmail = g.Key, OrderCount = g.Count() })
                .ToListAsync();

            return View(orderCounts);
       
        }*/

    }
}
