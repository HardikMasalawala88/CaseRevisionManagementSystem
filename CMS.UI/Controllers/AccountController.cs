using CMS.Data.FormModels;
using Microsoft.AspNetCore.Mvc;

namespace CMS.UI.Controllers
{
    public class AccountController : Controller
    {
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(LoginFM login)
        {
            return View();
        }

        [HttpGet]
        public IActionResult Registration()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Registration(RegisterFM register)
        {
            return View();
        }
    }
}
