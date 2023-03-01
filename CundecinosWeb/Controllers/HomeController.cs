using CundecinosWeb.Data;
using CundecinosWeb.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Security.Claims;

namespace CundecinosWeb.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly DataContext _context;

        public HomeController(ILogger<HomeController> logger,DataContext context)
        {
            _logger = logger;
            _context = context;
        }

        public IActionResult Index()
        {
            //var claims = User.Identities.ToList();

            var claim = User.FindFirstValue(ClaimTypes.NameIdentifier);
            //var claim = User.Claims.Where(x => x.ValueType == ClaimTypes.NameIdentifier);
            //var user = _context.People.Where(x => x.UID == Guid.Parse(claim)).FirstOrDefault();
            //if (user == null)
            //{
            //    return RedirectToAction("Registrer", "User");
            //}
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}