using CundecinosWeb.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace CundecinosWeb.Controllers
{
	public class PublicationController : Controller
	{
        private readonly DataContext _context;
        public PublicationController(DataContext context)
		{
			_context = context;

		}
		public IActionResult Index()
		{
			var model = _context.People.Include( x => x.CollegeCareer).Where(x => x.UID == Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier))).FirstOrDefault();
			return View(model);
		}
	}
}
