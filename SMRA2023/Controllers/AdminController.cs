using Microsoft.AspNetCore.Mvc;

namespace SMRA2023.Controllers
{
    public class AdminController : Controller
    {
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Admin()
        {
            return View("Admin","Home");
        }


        [HttpGet]
        public IActionResult Profile()
        {
            return View();
        }
    }

}
