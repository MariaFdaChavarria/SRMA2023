using Microsoft.AspNetCore.Mvc;

namespace SMRA2023.Controllers
{
    public class Employee : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Absence()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Edit()
        {
            return View();
        }

        [HttpGet]
        public IActionResult EmployeeInfo()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Vacation()
        {
            return View();
        }

        [HttpGet]
        public IActionResult VacationAdd()
        {
            return View();
        }

        [HttpGet]
        public IActionResult VacationRefund()
        {
            return View();
        }
    }
}

