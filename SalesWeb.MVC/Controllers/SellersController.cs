using Microsoft.AspNetCore.Mvc;
using SalesWeb.MVC.Models;
using SalesWeb.MVC.Models.ViewModels;
using SalesWeb.MVC.Services;

namespace SalesWeb.MVC.Controllers
{
    public class SellersController : Controller
    {
        private readonly SellerService _service;
        private readonly DepartmentService _departmentService;

        public SellersController(SellerService service, DepartmentService departmentService)
        {
            _service = service;
            _departmentService = departmentService;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var list = _service.GetAll();

            return View(list.OrderBy(s => s.Name));
        }

        [HttpGet]
        public IActionResult Create()
        {
            var model = new SellerViewModel
            {
                Departments = _departmentService.GetAll()
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("Name,Email,BirthDate,BaseSalary,DepartmentId")] Seller seller)
        {
            _service.Create(seller);

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public IActionResult Delete(int? id)
        {
            if (id is null)
                return NotFound();

            var seller = _service.Get(id.Value);

            if (seller is null)
                return NotFound();

            return View(seller);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int id)
        {
            _service.Delete(id);

            return RedirectToAction(nameof(Index));
        }
    }
}
