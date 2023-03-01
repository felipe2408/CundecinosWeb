using CundecinosWeb.Models;
using Microsoft.AspNetCore.Mvc;

namespace CundecinosWeb.Controllers
{
    public class UserController : Controller
    {
        public IActionResult PersonalInformation()
        {
            return View();
        }

        public IActionResult Registrer()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Registrer(Person person)
        {
            return RedirectToAction("SplashWelcomeCundecinos", "Splash");
        }
    }
}
