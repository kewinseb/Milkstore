using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MilkStore.Data;
using MilkStore.Models;

namespace MilkStore.Controllers
{
    public class Order_oldController : Controller
    {

        private readonly MilkstoreDbContext _context;
        private readonly ILogger<Order_oldController> _logger;

        public Order_oldController(MilkstoreDbContext context, ILogger<Order_oldController> logger)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }
        public IActionResult Index()
        {
            try
            {
                // Fetch all Orders from the database
                var Order = _context.Orders.ToList();

                // Check if no Orders exist
                if (Order == null || !Order.Any())
                {
                    ViewBag.Message = "Your order hasn't been placed yet! Check out the Orders and place your order now!";
                }

                return View(Order);
            }
            catch (Exception ex)
            {
                // Log the exception
                _logger.LogError(ex, "An error occurred while fetching Orders.");

                ViewBag.ErrorMessage = "An error occurred while fetching Orders.";
                return View(new List<Order_old>());
            }

        }
    }
}

