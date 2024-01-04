using Microsoft.AspNetCore.Mvc;
using SalesWeb.MVC.Models;
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

        [HttpGet]
        public IActionResult Index()
        {
            var list = _service.FindAll();

            return View(list.OrderBy(s => s.Name));
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("Name,Email,BirthDate,BaseSalary")]Seller model)
        {
            _service.Add(model);

            return RedirectToAction(nameof(Index));
        }
    }
}
