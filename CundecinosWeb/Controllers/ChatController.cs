using CundecinosWeb.Data;
using CundecinosWeb.Models;
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
            var claim = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var user = _context.People.Where(x => x.UID == Guid.Parse(claim)).FirstOrDefault();
            if (user == null)
            {
                return RedirectToAction("Register", "User");
            }
            ViewBag.UserId = user.UID;
            var people = _context.People.Where(x=>x.UID != user.UID).ToList();
            var model = new vChatUser
            {
                Person = user,
                People = people,
                ReceivedMessages = _context.Messages.Where(x => x.SenderID == user.UID && x.AddresseeID == people.First().UID).ToList(),
                SentMessages = _context.Messages.Where(x => x.SenderID == user.UID && x.AddresseeID == people.First().UID).ToList()
            };
            return View(model);
        }
        [HttpGet]
        public IActionResult ChatUser(Guid id)
        {
            var person = _context.People.Where(x => x.UID == id).FirstOrDefault();
            var people = _context.People.Where(x=>x.UID != id).ToList();

            var model = new vChatUser();
            model.Person.UID = id;
            model.Person = person;
            model.People = people;
            return View(model);
        }
        public async Task<IActionResult> SaveMessage(Message message)
        {

            return Ok();
        }
    }
}
