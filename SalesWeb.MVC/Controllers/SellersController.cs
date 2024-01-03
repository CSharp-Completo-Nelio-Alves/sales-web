using Microsoft.AspNetCore.Mvc;

namespace SalesWeb.MVC.Controllers
{
    public class SellersController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
