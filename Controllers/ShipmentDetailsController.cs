using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MilkStore.Data;
using MilkStore.Models;

public class ShipmentDetailsController : Controller
{
    private readonly MilkstoreDbContext _context;
    private readonly ILogger<ProductController> _logger;

    public ShipmentDetailsController(MilkstoreDbContext context, ILogger<ProductController> logger)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

     public IActionResult Index()
     {
         try
         {
             return View();
         }
         catch (Exception ex)
         {
             // Log the exception
             _logger.LogError(ex, "An error occurred while fetching products.");

             ViewBag.ErrorMessage = "An error occurred while fetching products.";
             return View(new List<Product>());
         }
     }

}


