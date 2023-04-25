using CundecinosWeb.Data;
using Microsoft.AspNetCore.Mvc;

namespace CundecinosWeb.Controllers
{
    public class ChatController : Controller
    {

        private readonly DataContext _context;
        public ChatController(DataContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var people = _context.People.ToList();
            return View(people);
        }
    }
}
