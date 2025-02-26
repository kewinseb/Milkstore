using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages.Manage;
using MilkStore;
using MilkStore.Data;
using MilkStore.Models;
using System.Security.Claims;

public class ProductController : Controller
{
    private readonly MilkstoreDbContext _context;
    private readonly ILogger<ProductController> _logger;

    public ProductController(MilkstoreDbContext context, ILogger<ProductController> logger)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    public IActionResult Index()
    {
        try
        {
            // Fetch all products from the database
            var products = _context.Products.ToList();

            // Check if no products exist
            if (products == null || !products.Any())
            {
                ViewBag.Message = "No products available.";
            }

            return View(products);
        }
        catch (Exception ex)
        {
            // Log the exception
            _logger.LogError(ex, "An error occurred while fetching products.");

            ViewBag.ErrorMessage = "An error occurred while fetching products.";
            return View(new List<Product>());
        }
    }

    [HttpGet]
    public async Task<IActionResult> GetCart()
    {
        string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (string.IsNullOrEmpty(userId))
        {
            return Unauthorized("User not logged in.");
        }
        var cartItems = await _context.Carts.Where(c => c.UserEmailId == userId).ToListAsync();
        return Ok(cartItems);
    }

    [HttpPost]
    [Route("ProductController/AddToCart")]

    public async Task<IActionResult> AddToCart([FromBody] Cart cartItem)
    {
        if (cartItem == null)
        {
            return BadRequest("Invalid cart data.");
        }

        try
        {
            cartItem.UserEmailId = HttpContext.Session.GetString("UserEmailId");

            var existingItem = await _context.Carts.FirstOrDefaultAsync(c => c.ProductProductId == cartItem.ProductProductId && c.UserEmailId == cartItem.UserEmailId); // Include UserEmailId in the check

            if (existingItem != null)
            {
                existingItem.Quantity += cartItem.Quantity;
                _context.Carts.Update(existingItem); // Update existing item
            }
            else
            {
                _context.Carts.Add(cartItem);
            }

            await _context.SaveChangesAsync();
            return Ok(new { Message = "Item added to cart successfully!" });
        }
        catch (DbUpdateException ex) // Catch DbUpdateException specifically
        {
            _logger.LogError(ex, "Error adding to cart (DbUpdateException)"); // Log full exception details
            return StatusCode(500, "A database error occurred while adding to cart."); // More specific message
        }
        catch (Exception ex) // Catch other exceptions
        {
            _logger.LogError(ex, "Error adding to cart"); // Log full exception details
            return StatusCode(500, "An error occurred while adding to cart."); // General message
        }

    }

    [HttpPut]
    [Route("ProductController/UpdateCartQuantity")]
    public async Task<IActionResult> UpdateCartQuantity([FromBody] Cart cartItem)
    {
        if (cartItem == null)
        {
            return BadRequest("Invalid cart data.");
        }

        try
        {
            cartItem.UserEmailId = HttpContext.Session.GetString("UserEmailId");

            var existingItem = await _context.Carts.FirstOrDefaultAsync(c => c.ProductProductId == cartItem.ProductProductId && c.UserEmailId == cartItem.UserEmailId);

            if (existingItem != null)
            {
                existingItem.Quantity = cartItem.Quantity;
                _context.Carts.Update(existingItem);
                await _context.SaveChangesAsync();
                return Ok(new { Message = "Cart quantity updated successfully!" });
            }

            return NotFound(new { Message = "Item not found in cart." });
        }
        catch (DbUpdateException ex)
        {
            _logger.LogError(ex, "Error updating cart quantity (DbUpdateException)");
            return StatusCode(500, "A database error occurred while updating cart quantity.");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error updating cart quantity");
            return StatusCode(500, "An error occurred while updating cart quantity.");
        }
    }

    [HttpDelete]
    [Route("ProductController/RemoveFromCart")]
    public async Task<IActionResult> RemoveFromCart([FromBody] int productProductId)

    {

        if (productProductId <= 0)

        {

            return BadRequest(new { Message = "Invalid product ID." });

        }

        var cartItem = await _context.Carts.FirstOrDefaultAsync(c => c.ProductProductId == productProductId);

        if (cartItem != null)

        {

            _context.Carts.Remove(cartItem);

            await _context.SaveChangesAsync();

            return Ok(new { Message = "Item removed from cart!" });

        }

        return NotFound(new { Message = "Item not found in cart." });

    }

}