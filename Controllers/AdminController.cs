using Microsoft.AspNetCore.Mvc;
using MilkStore.Models;

namespace MilkStore.Controllers
{
    public class AdminController : Controller
    {
            // Add Product - GET
            public IActionResult Add()
            {
                return View();
            }

            // Add Product - POST
            [HttpPost]
            [ValidateAntiForgeryToken]
            public IActionResult Add(Product product)
            {
                if (ModelState.IsValid)
                {
                    TempData["SuccessMessage"] = "Product added successfully!";
                    return RedirectToAction("Index");
                }

                return View(product);
            }
        }
    }


