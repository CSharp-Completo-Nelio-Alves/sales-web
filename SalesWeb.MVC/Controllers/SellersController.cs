using Microsoft.AspNetCore.Mvc;
using SalesWeb.MVC.Services;

namespace SalesWeb.MVC.Controllers
{
    public class SellersController : Controller
    {
        private readonly SellerService _service;

        public SellersController(SellerService service)
        {
            _service = service;
        }

        public IActionResult Index()
        {
            var list = _service.FindAll();

            return View(list.OrderBy(s => s.Name));
        }
    }
}
