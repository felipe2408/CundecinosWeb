using CundecinosWeb.Data;
using CundecinosWeb.Models;
using CundecinosWeb.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Security.Claims;

namespace CundecinosWeb.Controllers
{
    [Authorize]
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

            try
            {
                var claim = User.FindFirstValue(ClaimTypes.NameIdentifier);
                var user = _context.People.Where(x => x.UID == Guid.Parse(claim)).FirstOrDefault();
                if (user == null)
                {
                    return RedirectToAction("Register", "User");
                }
                var publications = _context.Publication.Where(x => x.PersonID == user.PersonID).OrderByDescending(x => x.PublicationDate).Take(10).ToList();
                var model = new vPersonPublication();
                model.Person = user;
                model.PublicationUsers = publications;

                return View(model);

            }
            catch (Exception)
            {

                return View();
            }
            
        }

        public IActionResult Privacy(Guid id)
        {

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            ViewBag.UserId = userId;
            //ViewBag.UserId = id;
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}