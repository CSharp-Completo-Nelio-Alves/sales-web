using Microsoft.AspNetCore.Mvc;
using SalesWeb.MVC.Helpers.ExtesionsMethods;
using SalesWeb.MVC.Models;
using SalesWeb.MVC.Models.ViewModels;
using SalesWeb.MVC.Services;

namespace SalesWeb.MVC.Controllers
{
    public class SellersController : BaseController
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
            if (!ModelState.IsValid)
            {
                ModelState.AddExceptionErrors();

                var model = new SellerViewModel
                {
                    Seller = seller,
                    Departments = _departmentService.GetAll(),
                };

                return View(model);
            }

            _service.Create(seller);

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public IActionResult Edit(int? id)
        {
            if (id is null)
                return RedirectToAction(nameof(Error), new { message = "Id not provided" });

            var seller = _service.Get(id.Value);

            if (seller is null)
                return RedirectToAction(nameof(Error), new { message = "Id not found" });

            var model = new SellerViewModel
            {
                Seller = seller,
                Departments = _departmentService.GetAll()
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, [Bind("Id,Name,Email,BirthDate,BaseSalary,DepartmentId")] Seller seller)
        {
            if (id != seller.Id)
                return RedirectToAction(nameof(Error), new { message = "Id mismatch" });

            if (!ModelState.IsValid)
            {
                ModelState.AddExceptionErrors();

                var model = new SellerViewModel
                {
                    Seller = seller,
                    Departments = _departmentService.GetAll(),
                };

                return View(model);
            }

            try
            {
                _service.Update(seller);

                return RedirectToAction(nameof(Index));
            }
            catch (ApplicationException ex)
            {
                return RedirectToAction(nameof(Error), new { message = ex.Message });
            }
        }

        [HttpGet]
        public IActionResult Details(int? id)
        {
            if (id is null)
                return RedirectToAction(nameof(Error), new { message = "Id not provided" });

            var seller = _service.Get(id.Value);

            if (seller is null)
                return RedirectToAction(nameof(Error), new { message = "Id not found" });

            return View(seller);
        }

        [HttpGet]
        public IActionResult Delete(int? id)
        {
            if (id is null)
                return RedirectToAction(nameof(Error), new { message = "Id not provided" });

            var seller = _service.Get(id.Value);

            if (seller is null)
                return RedirectToAction(nameof(Error), new { message = "Id not found" });

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
