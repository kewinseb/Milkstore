using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MilkStore.Data;
using MilkStore.Models;
using Newtonsoft.Json.Linq;
using System.Text.Json;

namespace MilkStore.Controllers
{
    public class PaymentController : Controller
    {
        private readonly MilkstoreDbContext _context;

        public PaymentController(MilkstoreDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [Route("PaymentController/SavePayment")]
        public IActionResult SavePayment([FromBody] JsonDocument data)
        {
            var userEmailId = HttpContext.Session.GetString("UserEmailId");
            var orderid = 0;

            if (data == null)
            {
                return BadRequest("Invalid request data.");
            }

            var root = data.RootElement;

            if (!root.TryGetProperty("paymentMethod", out JsonElement paymentMethodElement))
            {
                return BadRequest("Missing required fields.");
            }
            string cardNumber = null;
            string cvv = null;
            string paymentMethod = paymentMethodElement.GetString(); // Extract payment method

            if(paymentMethod == "debit-credit")
            {
                if (!root.TryGetProperty("cardNumber", out JsonElement cardNumberElement) || !root.TryGetProperty("cvv", out JsonElement cvvElement))
                {
                    return BadRequest("Missing card details for debit-credit payment.");
                }

                cardNumber = cardNumberElement.GetString();
                cvv = cvvElement.GetString();

                var cardDetail = new CardDetail
                {

                    CardNumber = EncryptionHelper.Encrypt(cardNumber),
                    Cvv = EncryptionHelper.Encrypt(cvv),
                    UserEmailId = userEmailId
                };

                _context.CardDetails.Add(cardDetail);
                _context.SaveChanges();
            }
            // Fetch recently stored cart data for the user
            var cartItems = _context.Carts.Where(c => c.UserEmailId == userEmailId).ToList();
            if (cartItems == null || !cartItems.Any())
            {
                return BadRequest("No items found in the cart to place an order");
            }

            // Store cart data into Order table
            foreach (var cartItem in cartItems)
            {
                var product = _context.Products.FirstOrDefault(p => p.ProductId == cartItem.ProductProductId);
                if (product == null)
                {
                    return BadRequest($"Product with ID {cartItem.ProductProductId} not found");
                }
                var orderStatus = paymentMethod == "debit-credit" ? "Shipped" : "Pending";
                var order = new Orders
                {
                    UserEmailId = userEmailId,
                    OrderQuantity = cartItem.Quantity,
                    TotalAmount =  product.Price, // Correct total amount calculation
                    OrderDate = DateTime.Now,
                    CreatedAt = cartItem.CreatedAt,
                    UpdatedAt = cartItem.UpdatedAt,
                    OrderStatus = orderStatus
                };
               orderid= _context.Orders.Add(order);
            }

            _context.SaveChanges();

            // Fetch user's recent orders
            var orders = _context.Orders.Where(c => c.UserEmailId == userEmailId).ToList();
            if (orders == null || !orders.Any())
            {
                return BadRequest("No transactions found in the table");
            }

            // Store order data into Transaction table
            foreach (var order in orders)
            {
                var transaction = new Transaction
                {
                    UserEmailId = userEmailId,
                    TotalAmountPaid = order.TotalAmount,
                    TransactionDate = order.OrderDate,
                    PaymentStatus = order.OrderStatus,
                    PaymentMode = paymentMethod
                };
                _context.Transactions.Add(transaction);
            }

            _context.SaveChanges();

            // **Delete Cart Items after Payment is Successful**
            _context.Carts.RemoveRange(cartItems);
            _context.SaveChanges();

            return Ok("Payment details saved, order placed successfully, and cart items cleared.");
        }
    }
}








