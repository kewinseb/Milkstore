using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MilkStore.Data;
using MilkStore.Models;

namespace MilkStore.Controllers
{
    public class OrderController : Controller
    {

        private readonly MilkstoreDbContext _context;
        private readonly ILogger<OrderController> _logger;

        public OrderController(MilkstoreDbContext context, ILogger<OrderController> logger)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }
        public IActionResult Index()
        {
            try
            {
                // Fetch all products from the database
                var Order = _context.Products.ToList();

                // Check if no products exist
                if (Order == null || !Order.Any())
                {
                    ViewBag.Message = "Your order hasn't been placed yet! Check out the product and place your order now!";
                }

                return View(Order);
            }
            catch (Exception ex)
            {
                // Log the exception
                _logger.LogError(ex, "An error occurred while fetching products.");

                ViewBag.ErrorMessage = "An error occurred while fetching products.";
                return View(new List<Order>());
            }

        }
    }
}

