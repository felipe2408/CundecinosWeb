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
			var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
			ViewBag.UserId = userId;
			var people = _context.People.Where(x => x.UID != Guid.Parse(userId)).OrderBy(x=>x.FirstName).ToList();
			return View(people);
		}
		[HttpGet]
		public IActionResult ChatUser(Guid id)
		{
			var person = _context.People.Where(x => x.UID == id).FirstOrDefault();
            var model = new Message();
            var sender = _context.People.Where(x => x.UID == Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier))).Include(x=>x.SentMessages).Include(x=>x.ReceivedMessages).First();
            model.SenderID = sender.PersonID;
            model.Sender = sender;
            model.Addressee = person;
			model.AddresseeID = person.PersonID;
			ViewBag.Messages = sender.SentMessages.Where(x=>x.SenderID == model.Sender.PersonID && x.AddresseeID == model.Addressee.PersonID).Concat(sender.ReceivedMessages.Where(x=>x.SenderID == model.Addressee.PersonID && x.AddresseeID == model.Sender.PersonID)).OrderBy(x=>x.SentAt).ToList();
            ViewBag.UserId = sender.PersonID;

            return View(model);
		}

		//[HttpPost]
		//public async Task SaveMessage(Message message)
  //      {
  //          try
  //          {
  //              _context.Messages.Add(message);
  //              await _context.SaveChangesAsync();
  //              //return Ok();//RedirectToAction("ChatUser",new { id = _context.People.Where(x=>x.PersonID==message.AddresseeID).First().UID});
  //          }
  //          catch (Exception ex)
  //          {
  //            //  return BadRequest(ex.Message);
  //          }
  //      }

    }
}
