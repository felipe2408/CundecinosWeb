using Microsoft.AspNetCore.Mvc;

namespace CundecinosWeb.Controllers
{
    public class SplashController : Controller
    {
        public IActionResult SplashWelcome()
        {
            return View();
        }
        public IActionResult SplashWelcomeCundecinos()
        {
            return RedirectToAction("SplashWelcome","Splash");
        }

    }
}
