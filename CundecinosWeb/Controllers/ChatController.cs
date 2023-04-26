using CundecinosWeb.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace CundecinosWeb.Controllers
{
    [Authorize]
    public class ChatController : Controller
    {

        private readonly DataContext _context;
        public ChatController(DataContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {


			var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
			ViewBag.UserId = userId;
			var people = _context.People.ToList();
            return View(people);
        }
    }
}
