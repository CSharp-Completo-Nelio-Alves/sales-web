using Microsoft.AspNetCore.Mvc;
using SalesWeb.MVC.Helpers;
using SalesWeb.MVC.Helpers.ExtesionsMethods;
using SalesWeb.MVC.Models;
using SalesWeb.MVC.Services;

namespace SalesWeb.MVC.Controllers
{
    public class DepartmentsController : BaseController
    {
        private readonly DepartmentService _service;

        private readonly string _defaultReturnRoute = $"/Departments";

        public DepartmentsController(DepartmentService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            return View(await _service.GetAllAsync());
        }

        [HttpGet]
        public async Task<IActionResult> Details(int? id)
        {
            if (id is null)
                return RedirectToAction(nameof(Error), new { message = ErrorMessagesHelper.IdNotProvided, returnUrl = _defaultReturnRoute });

            var department = await _service.GetAsync(id.Value);

            if (department is null)
                return RedirectToAction(nameof(Error), new { message = string.Format(ErrorMessagesHelper.EntityNotFound, nameof(Department)), returnUrl = _defaultReturnRoute });

            return View(department);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name")] Department department)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    await _service.CreateAsync(department);

                    return RedirectToAction(nameof(Index));
                }
                catch (ApplicationException ex)
                {
                    return RedirectToAction(nameof(Error), new { message = ex.Message, returnUrl = _defaultReturnRoute });
                }
            }

            ModelState.AddExceptionErrors();

            return View(department);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id is null)
                return RedirectToAction(nameof(Error), new { message = ErrorMessagesHelper.IdNotProvided, returnUrl = _defaultReturnRoute });

            var department = await _service.GetAsync(id.Value);

            if (department is null)
                return RedirectToAction(nameof(Error), new { message = string.Format(ErrorMessagesHelper.EntityNotFound, nameof(Department)), returnUrl = _defaultReturnRoute });

            return View(department);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name")] Department department)
        {
            if (id != department.Id)
                return RedirectToAction(nameof(Error), new { message = ErrorMessagesHelper.IdMistmatch });

            if (ModelState.IsValid)
            {
                try
                {
                    await _service.UpdateAsync(department);
                }
                catch (ApplicationException ex)
                {
                    return RedirectToAction(nameof(Error), new { message = ex.Message, returnUrl = _defaultReturnRoute });
                }

                return RedirectToAction(nameof(Index));
            }

            ModelState.AddExceptionErrors();

            return View(department);
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id is null)
                return RedirectToAction(nameof(Error), new { message = ErrorMessagesHelper.IdNotProvided, returnUrl = _defaultReturnRoute });

            var department = await _service.GetAsync(id.Value);

            if (department is null)
                return RedirectToAction(nameof(Error), new { message = string.Format(ErrorMessagesHelper.EntityNotFound, nameof(Department)), returnUrl = _defaultReturnRoute });

            return View(department);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            try
            {
                var department = await _service.GetAsync(id);

                if (department is null)
                    return RedirectToAction(nameof(Error), new { message = string.Format(ErrorMessagesHelper.EntityNotFound, nameof(Department)), returnUrl = _defaultReturnRoute });

                await _service.DeleteAsync(department);
            }
            catch (ApplicationException ex)
            {
                return RedirectToAction(nameof(Error), new { message = ex.Message, returnUrl = _defaultReturnRoute });
            }

            return RedirectToAction(nameof(Index));
        }
    }
}
