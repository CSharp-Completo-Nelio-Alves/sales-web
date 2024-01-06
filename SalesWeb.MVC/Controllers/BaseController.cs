using Microsoft.AspNetCore.Mvc;
using SalesWeb.MVC.Models.ViewModels;
using System.Diagnostics;

namespace SalesWeb.MVC.Controllers
{
    public abstract class BaseController : Controller
    {
        public IActionResult Error(string message, string returnUrl = null)
        {
            var error = new ErrorViewModel()
            {
                RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier,
                Message = message
            };

            if (string.IsNullOrWhiteSpace(returnUrl))
                returnUrl = "/";

            ViewData["ReturnUrl"] = returnUrl;

            return View(error);
        }
    }
}
