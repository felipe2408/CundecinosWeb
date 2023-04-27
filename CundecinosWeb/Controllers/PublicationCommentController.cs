using CundecinosWeb.Data;
using Microsoft.AspNetCore.Mvc;

namespace CundecinosWeb.Controllers
{
	public class PublicationCommentController : Controller
	{

		private readonly DataContext _context;

		public PublicationCommentController(DataContext context)
		{
			_context = context;
		}
		public IActionResult PublicationDescription()
		{
			return View();
		}
	}
}
