using Microsoft.AspNetCore.Mvc;
using MilkStore.Data;
using MilkStore.Models;


namespace MilkStore.Controllers
   
{
    public class TransactionController : Controller
    {
        private readonly MilkDairyDbContext _context;

        public TransactionController(MilkDairyDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View(new List<Transaction>());
        }

        [HttpPost]
        public IActionResult Search(string transactionId, DateTime? fromDate, DateTime? toDate)
        {
            if (!ValidateSearchFields(transactionId, fromDate, toDate, out string errorMessage))
            {
                ViewBag.ErrorMessage = errorMessage;
                return View("Index", new List<Transaction>());
            }

            var filteredTransactions = fetchTransactionHistory(transactionId, fromDate, toDate);
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

            if (!string.IsNullOrWhiteSpace(transactionId) && transactionId.Length < 5)
            {
                errorMessage = "Transaction ID must be at least 5 characters.";
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
