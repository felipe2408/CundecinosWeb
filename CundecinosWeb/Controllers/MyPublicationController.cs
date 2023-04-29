using CundecinosWeb.Data;
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
            var publications = _context.Publication.Where(x => x.PersonID == user.PersonID).OrderByDescending(x => x.PublicationDate).Include(x => x.PublicationAttachment);
            return View(await publications.ToListAsync());
		}
        public async Task<IActionResult> EditPublication(Guid id)
        {

            return View();
        }
        public async Task<IActionResult> HidePublication(Guid id)
        {

            return RedirectToAction("Index");
        }
	}
}
