using Microsoft.AspNetCore.Mvc;
using MilkStore.Data;
using MilkStore.Models;
using System.Collections.Generic;
using System.Linq;

namespace MilkStore.Controllers
{
    public class ProductController : Controller
    {
        private readonly MilkstoreDbContext _context;

        public ProductController(MilkstoreDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            // Fetch all products from the database
            var products = _context.Products.ToList();

            return View(products);
        }
    }
}
