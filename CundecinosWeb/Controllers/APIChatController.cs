using CundecinosWeb.Data;
using CundecinosWeb.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CundecinosWeb.Controllers
{
    public class APIChatController : Controller
    {
        private DataContext _context;
        public APIChatController(DataContext context)
        {
            _context = context;
        }
        [Route("/[controller]/[action]")]
        [HttpPost]
        public async Task<IActionResult> SaveMessage([FromBody]Message message)
        {
            try
            {
                var senderID = _context.People.Where(p => p.UID == message.SenderID).First().PersonID;
                var addressedID = _context.People.Where(p => p.UID == message.AddresseeID).First().PersonID;
                message.SenderID = senderID;
                message.AddresseeID = addressedID;
                _context.Messages.Add(message);
                await _context.SaveChangesAsync();
                return Json(new { success = true });
            }
            catch (Exception ex)
            {
                return Json(new { success = false });
            }
            
        }
        //[HttpGet]
        //public async Task<IActionResult> UnreadMessages([FromRoute]string UserID)
        //{
        //    int Count = 0;
        //    var ID = _context.People.Where(x=>x.UID == Guid.Parse(UserID)).First().PersonID;
        //    Count = _context.Messages.Where(x => x.AddresseeID == ID).Count();
        //    return Json(new { messages = Count });
        //}
    }
}
