using Microsoft.AspNetCore.Mvc;
using MilkStore.Models;

namespace MilkStore.Controllers
{
    public class AdminController : Controller
    {
        private static List<Product> _products = new List<Product>();
        public IActionResult Index(string actionType, int? id)
        {
            ViewBag.ActionType = actionType;

            if (actionType == "Edit" && id.HasValue)
            {
                var product = _products.FirstOrDefault(p => p.ProductId == id.Value);
                return View(product);
            }
            else if (actionType == "Delete" && id.HasValue)
            {
                var product = _products.FirstOrDefault(p => p.ProductId == id.Value);
                return View(product);
            }

            return View(new Product());
        }

        [HttpPost]
        public IActionResult Add(Product product)
        {
            product.ProductId = _products.Max(p => p.ProductId) + 1;
            product.CreatedAt = DateTime.Now;
            product.UpdatedAt = DateTime.Now;
            _products.Add(product);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult Edit(Product product)
        {
            var existingProduct = _products.FirstOrDefault(p => p.ProductId == product.ProductId);
            if (existingProduct != null)
            {
                existingProduct.ProductName = product.ProductName;
                existingProduct.Price = product.Price;
                existingProduct.StockQuantity = product.StockQuantity;
                existingProduct.Category = product.Category;
                existingProduct.EstimatedDelivery = product.EstimatedDelivery;
                existingProduct.UpdatedAt = DateTime.Now;
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult Delete(int id)
        {
            var product = _products.FirstOrDefault(p => p.ProductId == id);
            if (product != null)
            {
                _products.Remove(product);
            }
            return RedirectToAction("Index");
        }
    }

}
    

