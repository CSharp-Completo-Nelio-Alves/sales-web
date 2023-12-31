﻿using Microsoft.AspNetCore.Mvc;
using SalesWeb.MVC.Helpers;
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

        private readonly string _defaultReturnRoute = "/Sellers";

        public SellersController(SellerService service, DepartmentService departmentService)
        {
            _service = service;
            _departmentService = departmentService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var list = await _service.GetAllAsync();

            return View(list.OrderBy(s => s.Name));
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var model = new SellerViewModel
            {
                Departments = await _departmentService.GetAllAsync()
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name,Email,BirthDate,BaseSalary,DepartmentId")] Seller seller)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddExceptionErrors();

                var model = new SellerViewModel
                {
                    Seller = seller,
                    Departments = await _departmentService.GetAllAsync(),
                };

                return View(model);
            }

            await _service.CreateAsync(seller);

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id is null)
                return RedirectToAction(nameof(Error), new { message = ErrorMessagesHelper.IdNotProvided, returnUrl = _defaultReturnRoute });

            var seller = await _service.GetAsync(id.Value);

            if (seller is null)
                return RedirectToAction(nameof(Error), new { message = string.Format(ErrorMessagesHelper.EntityNotFound, nameof(Seller)), returnUrl = _defaultReturnRoute });

            var model = new SellerViewModel
            {
                Seller = seller,
                Departments = await _departmentService.GetAllAsync()
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Email,BirthDate,BaseSalary,DepartmentId")] Seller seller)
        {
            if (id != seller.Id)
                return RedirectToAction(nameof(Error), new { message = ErrorMessagesHelper.IdMistmatch, returnUrl = _defaultReturnRoute });

            if (!ModelState.IsValid)
            {
                ModelState.AddExceptionErrors();

                var model = new SellerViewModel
                {
                    Seller = seller,
                    Departments = await _departmentService.GetAllAsync(),
                };

                return View(model);
            }

            try
            {
                await _service.UpdateAsync(seller);

                return RedirectToAction(nameof(Index));
            }
            catch (ApplicationException ex)
            {
                return RedirectToAction(nameof(Error), new { message = ex.Message, returnUrl = _defaultReturnRoute });
            }
        }

        [HttpGet]
        public async Task<IActionResult> Details(int? id)
        {
            if (id is null)
                return RedirectToAction(nameof(Error), new { message = ErrorMessagesHelper.IdNotProvided, returnUrl = _defaultReturnRoute });

            var seller = await _service.GetAsync(id.Value);

            if (seller is null)
                return RedirectToAction(nameof(Error), new { message = string.Format(ErrorMessagesHelper.EntityNotFound, nameof(Seller)), returnUrl = _defaultReturnRoute });

            return View(seller);
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id is null)
                return RedirectToAction(nameof(Error), new { message = ErrorMessagesHelper.IdNotProvided, returnUrl = _defaultReturnRoute });

            var seller = await _service.GetAsync(id.Value);

            if (seller is null)
                return RedirectToAction(nameof(Error), new { message = string.Format(ErrorMessagesHelper.EntityNotFound, nameof(Seller)), returnUrl = _defaultReturnRoute });

            return View(seller);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _service.DeleteAsync(id);

                return RedirectToAction(nameof(Index));
            }
            catch (ApplicationException ex)
            {
                return RedirectToAction(nameof(Error), new { message = ex.Message, returnUrl = _defaultReturnRoute });
            }
        }
    }
}
