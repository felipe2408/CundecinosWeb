using CundecinosWeb.Data;
using CundecinosWeb.Enum;
using CundecinosWeb.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace CundecinosWeb.Controllers
{
    public class InofferPublicationController : Controller
    {
        private readonly DataContext _context;

        public InofferPublicationController(DataContext context) { 
        
            _context = context;
        }   
        

        //Mis ofertas a las publicaciones
        public IActionResult MyOffers()
        {
            var claim = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var user = _context.People.Where(x => x.UID == Guid.Parse(claim)).FirstOrDefault();

            if (user == null)
            {

                return RedirectToAction("Register","User");
            }

            var myOffers =  _context.PublicationComments.Include(x => x.CommentAttachment).Include(x => x.Person).Where(x => x.PersonID == user.PersonID).ToList();

            return View(myOffers);
        }

        public IActionResult AcceptOffers(Guid id)
        {

            var comment = _context.PublicationComments.Where(x => x.PublicationCommentsID == id).FirstOrDefault();


            var publicaction = _context.Publication.Where(x => x.PublicationID == comment.PublicationID).FirstOrDefault();

            publicaction.Status = Status.Exchanged;

            comment.StatusInnofer = Enum.StatusInnofer.Completed;

            _context.Entry(comment).State = EntityState.Modified;
            _context.Entry(publicaction).State = EntityState.Modified;

            _context.SaveChanges();
            return RedirectToAction("MyOffers", "InofferPublication");
        }

        public IActionResult MyOffersAccept()
        {
            var claim = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var user = _context.People.Where(x => x.UID == Guid.Parse(claim)).FirstOrDefault();

            if (user == null)
            {

                return RedirectToAction("Register", "User");
            }

            var myOffers = _context.PublicationComments.Include(x => x.CommentAttachment).Include(x => x.Person).Include(x => x.Publication).Where(x => x.PersonID == user.PersonID && x.StatusInnofer == StatusInnofer.Completed).ToList();

            return View(myOffers);
        }



    }
}
