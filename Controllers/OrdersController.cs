using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MilkStore.Data;
using MilkStore.Models;

namespace MilkStore.Controllers
{
    public class OrdersController : Controller
    {
        private readonly MilkstoreDbContext _context;
        private bool isCancelOrder = false;
        private IQueryable<Orders> ordersQuery;

        public OrdersController(MilkstoreDbContext context)
        {
            _context = context;
        }

        // GET: Orders
        //public async Task<IActionResult> Index()
        //{
        //    return View(await _context.Order.ToListAsync());
        //}

        public async Task<IActionResult> Index(string status = "All", string orderIdSearch = "", DateTime? startDate = null, DateTime? endDate = null)
        {
           
            ordersQuery = _context.Orders.AsQueryable();
            // Get query parameters from HttpContext
            var queryStartDate = HttpContext.Request.Query["startDate"];
            var queryEndDate = HttpContext.Request.Query["endDate"];

            // Filter by order status
            if (status != "All")
            {
                ordersQuery = ordersQuery.Where(o => o.OrderStatus == status);
            }
            else
            {
                ordersQuery = ordersQuery.Where(i => i.OrderStatus != "Cancelled").AsQueryable();
            }

            // Search by Order ID
            if (!string.IsNullOrEmpty(orderIdSearch))
            {
                ordersQuery = ordersQuery.Where(o => o.OrderId.ToString().Contains(orderIdSearch));
            }

            // Filter by date range (Order Date)
            if (DateTime.TryParse(queryStartDate, out DateTime parsedStartDate) && DateTime.TryParse(queryEndDate, out DateTime parsedEndDate))
            {
                ordersQuery = ordersQuery.Where(o => o.OrderDate >= parsedStartDate && o.OrderDate <= parsedEndDate);
            }
            isCancelOrder = false;
            var orders = await ordersQuery.ToListAsync();
            return View(orders);


        }


        // Method to cancel an order
        public async Task<IActionResult> CancelOrder(int orderId)
        {
            var order = await _context.Orders.FindAsync(orderId);
            if (order != null)
            {

                order.OrderStatus = "Cancelled";
                await _context.SaveChangesAsync();
            }
            isCancelOrder = true;
            return RedirectToAction(nameof(Index));
        }

        // Method to reorder an order
        public async Task<IActionResult> Reorder(int orderId)
        {
            var order = await _context.Orders.FindAsync(orderId);
            if (order != null)
            {
                var newOrder = new Orders
                {
                    OrderDate = DateTime.Now,
                    DeliveryDate = DateTime.Now.AddDays(2),  // Delivery 2 days after reorder
                    TotalAmount = order.TotalAmount,
                    OrderStatus = "Pending",
                    ShippingAddress = order.ShippingAddress,
                    ProductName = order.ProductName,
                    ProductImage = order.ProductImage
                };
                _context.Orders.Add(newOrder);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));

        }


        // GET: Orders/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var order = await _context.Order
                .FirstOrDefaultAsync(m => m.OrderId == id);
            if (order == null)
            {
                return NotFound();
            }

            return View(order);
        }

        // GET: Orders/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Orders/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("OrderId,OrderDate,DeliveryDate,TotalAmount,OrderStatus,ShippingAddress,ProductName,ProductImage")] Orders order)
        {
            if (ModelState.IsValid)
            {
                _context.Add(order);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(order);
        }

        // GET: Orders/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var order = await _context.Order.FindAsync(id);
            if (order == null)
            {
                return NotFound();
            }
            return View(order);
        }

        // POST: Orders/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("OrderId,OrderDate,DeliveryDate,TotalAmount,OrderStatus,ShippingAddress,ProductName,ProductImage")] Orders order)
        {
            if (id != order.OrderId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(order);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OrderExists(order.OrderId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(order);
        }

        // GET: Orders/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var order = await _context.Order
                .FirstOrDefaultAsync(m => m.OrderId == id);
            if (order == null)
            {
                return NotFound();
            }

            return View(order);
        }

        // POST: Orders/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var order = await _context.Order.FindAsync(id);
            if (order != null)
            {
                _context.Order.Remove(order);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool OrderExists(int id)
        {
            return _context.Order.Any(e => e.OrderId == id);
        }
    }
}
