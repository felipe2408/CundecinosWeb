using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CundecinosWeb.Controllers
{
    [Authorize]
    public class SplashController : Controller
    {
        public IActionResult SplashWelcome()
        {
            return View();
        }
        

    }
}
