using Microsoft.AspNetCore.Mvc;
using SalesWeb.MVC.Models;

namespace SalesWeb.MVC.Controllers
{
    public class DepartmentController : Controller
    {
        public IActionResult Index()
        {
            var list = new List<Department>()
            {
                new("Tecnologia") { Id = 1 },
                new("Limpeza") { Id = 2 },
                new("Lactíneos") { Id = 3 }
            };

            return View(list);
        }
    }
}
