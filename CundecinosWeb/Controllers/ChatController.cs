using CundecinosWeb.Data;
using CundecinosWeb.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
        [HttpGet]
        public IActionResult ChatUser(Guid id)
        {
            var person = _context.People.Where(x => x.UID == id).FirstOrDefault();
            var people = _context.People.ToList();

            var model = new vChatUser();
            model.UID = id;
            model.Person = person;
            model.People = people;
            return View(model);
        }

    }
}
