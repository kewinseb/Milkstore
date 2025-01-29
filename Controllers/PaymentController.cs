using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MilkStore.Data;
using MilkStore.Models;

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
        public IActionResult SavePayment([FromBody] CardDetail request)
        {

            var userEmailId = HttpContext.Session.GetString("UserEmailId");
            if (string.IsNullOrWhiteSpace(request.CardNumber) || request.CardNumber.Length != 16)
            {
                return BadRequest("Invalid card number");
            }

            var cardDetail = new CardDetail
            {
                CardNumber = EncryptionHelper.Encrypt(request.CardNumber),
                Cvv = EncryptionHelper.Encrypt(request.Cvv),
                UserEmailId = userEmailId
            };

            _context.CardDetails.Add(cardDetail);
            _context.SaveChanges();

            return Ok("Payment details saved successfully");
        }
    }
}

 




