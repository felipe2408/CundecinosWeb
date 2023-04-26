using CundecinosWeb.Models;
using Microsoft.AspNetCore.Mvc;

namespace CundecinosWeb.Controllers
{
	public class APIReportController : Controller
	{

		[HttpGet]
		public async Task<IActionResult> ReportPublication()
		{
			return View();
		}
	}
}
