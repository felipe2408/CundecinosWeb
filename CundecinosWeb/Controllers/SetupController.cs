using CundecinosWeb.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CundecinosWeb.Controllers
{
    public class SetupController : Controller
    {
        private readonly DataContext _context;

        public SetupController(DataContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Extension()
        {
            return View();
        }

        public IActionResult CollegeCareer()
        {
            return View();
        }

        public IActionResult Users()
        {
             var users = _context.People.Include(x => x.CollegeCareer).Include(x => x.Extension).ToList();

            return View(users);
        }
    }
}
