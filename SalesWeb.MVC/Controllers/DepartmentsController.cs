﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SalesWeb.MVC.Helpers.ExtesionsMethods;
using SalesWeb.MVC.Models;
using SalesWeb.MVC.Services;

namespace SalesWeb.MVC.Controllers
{
    public class DepartmentsController : BaseController
    {
        private readonly DepartmentService _service;

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
                return RedirectToAction(nameof(Error), new { message = "Id not provided" });

            var department = await _service.GetAsync(id.Value);

            if (department is null)
                return RedirectToAction(nameof(Error), new { message = "Id not found" });

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
                    return RedirectToAction(nameof(Error), new { message = ex.Message });
                }
            }

            ModelState.AddExceptionErrors();

            return View(department);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id is null)
                return RedirectToAction(nameof(Error), new { message = "Id not provided" });

            var department = await _service.GetAsync(id.Value);

            if (department is null)
                return RedirectToAction(nameof(Error), new { message = "Id not found" });

            return View(department);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name")] Department department)
        {
            if (id != department.Id)
                return RedirectToAction(nameof(Error), new { message = "Id mismatch" });

            if (ModelState.IsValid)
            {
                try
                {
                    await _service.UpdateAsync(department);
                }
                catch (ApplicationException ex)
                {
                    return RedirectToAction(nameof(Error), new { message = ex.Message });
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
                return RedirectToAction(nameof(Error), new { message = "Id not provided" });

            var department = await _service.GetAsync(id.Value);

            if (department is null)
                return RedirectToAction(nameof(Error), new { message = "Id not found" });

            return View(department);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var department = await _service.GetAsync(id);

            if (department is null)
                return RedirectToAction(nameof(Error), new { message = "Id not found" });

            await _service.DeleteAsync(department);

            return RedirectToAction(nameof(Index));
        }
    }
}
