using Microsoft.AspNetCore.Mvc;
using MilkStore.Data;
using MilkStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MilkStore.Controllers
{
    public class TransactionController : Controller
    {
        private readonly MilkstoreDbContext _context;

        public TransactionController(MilkstoreDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            // Fetch all transactions to display on the page
            var allTransactions = _context.Transactions.ToList();
            return View(allTransactions);
        }

        [HttpPost]
        public IActionResult Search(string transactionId, DateTime? fromDate, DateTime? toDate)
        {
            if (!ValidateSearchFields(transactionId, fromDate, toDate, out string errorMessage))
            {
                ViewBag.ErrorMessage = errorMessage;
                ViewBag.TransactionId = transactionId;
                ViewBag.FromDate = fromDate?.ToString("yyyy-MM-dd");
                ViewBag.ToDate = toDate?.ToString("yyyy-MM-dd");
                return View("Index", _context.Transactions.ToList());
            }

            var filteredTransactions = fetchTransactionHistory(transactionId, fromDate, toDate);
            ViewBag.TransactionId = transactionId;
            ViewBag.FromDate = fromDate?.ToString("yyyy-MM-dd");
            ViewBag.ToDate = toDate?.ToString("yyyy-MM-dd");
            return View("Index", filteredTransactions);
        }

        private bool ValidateSearchFields(string transactionId, DateTime? fromDate, DateTime? toDate, out string errorMessage)
        {
            errorMessage = string.Empty;

            if (fromDate.HasValue && toDate.HasValue && fromDate > toDate)
            {
                errorMessage = "From Date must be earlier than or equal to To Date.";
                return false;
            }

            if (!string.IsNullOrWhiteSpace(transactionId) && (transactionId.Length != 1 || !int.TryParse(transactionId, out _)))
            {
                errorMessage = "Transaction ID must be a single numeric digit.";
                return false;
            }

            return true;
        }

        private IEnumerable<Transaction> fetchTransactionHistory(string transactionId, DateTime? fromDate, DateTime? toDate)
        {
            int? parsedTransactionId = null;
            if (!string.IsNullOrWhiteSpace(transactionId) && int.TryParse(transactionId, out int id))
            {
                parsedTransactionId = id;
            }

            return _context.Transactions.Where(t =>
                (!parsedTransactionId.HasValue || t.TransactionId == parsedTransactionId.Value) &&
                (!fromDate.HasValue || t.TransactionDate.Date >= fromDate.Value.Date) &&
                (!toDate.HasValue || t.TransactionDate.Date <= toDate.Value.Date)
            ).ToList();
        }

        [HttpPost]
        public IActionResult Clear()
        {
            return RedirectToAction("Index");
        }
    }
}
