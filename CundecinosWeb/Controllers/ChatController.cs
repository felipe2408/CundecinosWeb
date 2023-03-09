using Microsoft.AspNetCore.Mvc;

namespace CundecinosWeb.Controllers
{
    public class ChatController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
