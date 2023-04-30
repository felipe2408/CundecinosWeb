using CundecinosWeb.Data;
using CundecinosWeb.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace CundecinosWeb.Controllers
{
	public class MyPublicationController : Controller
	{
        private DataContext _context;

        public MyPublicationController(DataContext context)
        {

            _context = context;
        }
        public async Task<IActionResult> Index()
		{
            var claim = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var user = _context.People.Where(x => x.UID == Guid.Parse(claim)).FirstOrDefault();

            if (user == null)
            {
                return RedirectToAction("Register","User");
            }
            var publications = await _context.Publication.Where(x => x.PersonID == user.PersonID && x.IsActive == true).OrderByDescending(x => x.PublicationDate).Include(x => x.PublicationAttachment).ToListAsync();
            return View(publications.Count == 0 ? null : publications);
		}
        public async Task<IActionResult> EditPublication(Guid id)
        {
            var publication = await _context.Publication.Where(x => x.PublicationID == id).Include(x => x.Person).FirstOrDefaultAsync();
            return View(publication);
        }
        public async Task<IActionResult> HidePublication(Guid id)
        {
            var claim = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var user = _context.People.Where(x => x.UID == Guid.Parse(claim) && x.IsActive == true).FirstOrDefault();
            var publication = _context.Publication.Where(x => x.PublicationID == id).AsNoTracking().FirstOrDefault();
            publication.IsActive = false;
            _context.Entry(publication).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }
        public async Task<IActionResult> DeleteImage(Guid id, Guid publication)
        {
            _context.PublicationAttachments.Remove(new PublicationAttachment { PublicationAttachmentID = id });
            await _context.SaveChangesAsync();
            return RedirectToAction("EditPublication", new { id = publication });
        }
    }
}
