using Microsoft.AspNetCore.Mvc;
using MilkStore.Data;
using MilkStore.Models;


namespace MilkStore.Controllers
{
    public class AdminController : Controller
    {
        private readonly MilkstoreDbContext _context;

        public AdminController(MilkstoreDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var products = _context.Products.ToList();
            return View(products);
        }

        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Add(Product product, IFormFile productImage)
        {
            if (ModelState.IsValid)
            {
                if (productImage != null && productImage.Length > 0)
                {
                    string fileName = Guid.NewGuid().ToString() + Path.GetExtension(productImage.FileName);
                    string filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images", fileName);

                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        productImage.CopyTo(stream);
                    }
                    product.ProductImage = "/images/" + fileName;
                }

                product.CreatedAt = DateTime.Now;
                product.UpdatedAt = DateTime.Now;

                _context.Products.Add(product);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(product);
        }

        public IActionResult Edit(int? id)
        {
            if (id == null)
                return NotFound();

            var product = _context.Products.Find(id);
            if (product == null)
                return NotFound();

            return View(product);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Product product, IFormFile? productImage)
        {
            if (ModelState.IsValid)
            {
                var existingProduct = _context.Products.Find(product.ProductId);
                if (existingProduct == null)
                    return NotFound();

                if (productImage != null)
                {
                    string fileName = Guid.NewGuid().ToString() + Path.GetExtension(productImage.FileName);
                    string filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images", fileName);

                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        productImage.CopyTo(stream);
                    }

                    existingProduct.ProductImage = "/images/" + fileName;
                }

                existingProduct.ProductName = product.ProductName;
                existingProduct.Price = product.Price;
                existingProduct.StockQuantity = product.StockQuantity;
                existingProduct.Category = product.Category;
                existingProduct.UpdatedAt = DateTime.Now;

                _context.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(product);
        }

        [HttpPost]
        public IActionResult Delete(int id)
        {
            var product = _context.Products.Find(id);
            if (product == null)
                return NotFound();

            if (!string.IsNullOrEmpty(product.ProductImage))
            {
                string filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", product.ProductImage.TrimStart('/'));
                if (System.IO.File.Exists(filePath))
                    System.IO.File.Delete(filePath);
            }

            _context.Products.Remove(product);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
