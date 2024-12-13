using Microsoft.AspNetCore.Mvc;

namespace MilkStore.Controllers
{
    public class PaymentController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
