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
            var person = _context.People.Where(x => x.UID == Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier))).Include(x=>x.ReceivedMessages).First();
            if (person == null)
            {
                return RedirectToAction("Register","User");
            }
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var model = new vChat()
            {
                People = _context.People.Where(x => x.UID != Guid.Parse(userId)).OrderBy(x => x.FirstName).ToList(),
                PeopleWritten = person.ReceivedMessages.Where(x => x.Read == false).Select(x => x.Sender).ToList()
            };
			ViewBag.UserId = userId;
			return View(model);
		}
		[HttpGet]
		public async Task<IActionResult> ChatUser(Guid id)
		{
			var person = _context.People.Where(x => x.UID == id).Include(x=>x.ReceivedMessages).FirstOrDefault();
            if (person == null)
            {
                return RedirectToAction("Register", "User");
            }

            var model = new Message();
            var sender = _context.People.Where(x => x.UID == Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier))).Include(x=>x.SentMessages).Include(x=>x.ReceivedMessages).First();
            model.SenderID = sender.PersonID;
            model.Sender = sender;
            model.Addressee = person;
			model.AddresseeID = person.PersonID;
			ViewBag.Messages = sender.SentMessages.Where(x=>x.SenderID == model.Sender.PersonID && x.AddresseeID == model.Addressee.PersonID).Concat(sender.ReceivedMessages.Where(x=>x.SenderID == model.Addressee.PersonID && x.AddresseeID == model.Sender.PersonID)).OrderBy(x=>x.SentAt).ToList();
            ViewBag.UserId = sender.PersonID;
            foreach (var message in sender.ReceivedMessages)
            {
                if(!message.Read) 
                {
                    message.Read = true;
                }
            }
            _context.UpdateRange(sender.ReceivedMessages);
            await _context.SaveChangesAsync();
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
