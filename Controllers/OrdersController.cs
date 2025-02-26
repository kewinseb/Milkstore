using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MilkStore.Data;
using MilkStore.Models;

namespace MilkStore.Controllers
{
    public class OrdersController : Controller
    {
        private readonly MilkstoreDbContext _context;

        public OrdersController(MilkstoreDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index(string status = "All", string orderIdSearch = "", DateTime? startDate = null, DateTime? endDate = null)
        {
            var ordersQuery = from o in _context.Orders
                              join oi in _context.OrderItems on o.OrderId equals oi.OrdersOrderId
                              join p in _context.Products on oi.ProductProductId equals p.ProductId
                              group new { o, oi, p } by new
                              {
                                  o.OrderId,
                                  o.OrderDate,
                                  p.EstimatedDelivery,
                                  o.OrderStatus,
                                  o.UserEmailId,
                                  p.Price,
                                  p.ProductName,
                                  p.ProductImage
                              } into grouped
                              select new OrderDetails
                              {
                                  OrderId = grouped.Key.OrderId,
                                  OrderDate = grouped.Key.OrderDate,
                                  DeliveryDate = grouped.Key.EstimatedDelivery,
                                  OrderStatus = grouped.Key.OrderStatus,
                                  UserEmailId = grouped.Key.UserEmailId,
                                  TotalAmount = grouped.Key.Price,
                                  ProductName = grouped.Key.ProductName,
                                  ProductImage = grouped.Key.ProductImage
                              };

            // Apply filters
            if (status != "All")
                ordersQuery = ordersQuery.Where(o => o.OrderStatus == status);
            else
                ordersQuery = ordersQuery.Where(o => o.OrderStatus != "Cancelled");

            if (!string.IsNullOrEmpty(orderIdSearch))
                ordersQuery = ordersQuery.Where(o => o.OrderId.ToString().Contains(orderIdSearch));

            if (startDate.HasValue && endDate.HasValue)
                ordersQuery = ordersQuery.Where(o => o.OrderDate >= startDate.Value && o.OrderDate <= endDate.Value);

            var orders = await ordersQuery.ToListAsync();
            return View(orders);
        }

        public async Task<IActionResult> CancelOrder(int orderId)
        {
            var order = await _context.Orders.FindAsync(orderId);
            if (order != null)
            {
                order.OrderStatus = "Cancelled";
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Reorder(int orderId)
        {
            var order = await _context.Orders.FindAsync(orderId);
            if (order != null)
            {
                // Placeholder for reorder logic
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();
            var order = await _context.Orders.FirstOrDefaultAsync(m => m.OrderId == id);
            return order == null ? NotFound() : View(order);
        }

        public IActionResult Create() => View();

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("OrderId,OrderDate,DeliveryDate,TotalAmount,OrderStatus,UserEmailId")] Orders order)
        {
            if (ModelState.IsValid)
            {
                _context.Add(order);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(order);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();
            var order = await _context.Orders.FindAsync(id);
            return order == null ? NotFound() : View(order);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("OrderId,OrderDate,DeliveryDate,TotalAmount,OrderStatus,UserEmailId")] Orders order)
        {
            if (id != order.OrderId) return NotFound();
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(order);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OrderExists(order.OrderId)) return NotFound();
                    throw;
                }
                return RedirectToAction(nameof(Index));
            }
            return View(order);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();
            var order = await _context.Orders.FirstOrDefaultAsync(m => m.OrderId == id);
            return order == null ? NotFound() : View(order);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var order = await _context.Orders.FindAsync(id);
            if (order != null)
            {
                _context.Orders.Remove(order);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }

        private bool OrderExists(int id) => _context.Orders.Any(e => e.OrderId == id);
    }
}