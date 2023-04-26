using CundecinosWeb.Data;
using CundecinosWeb.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace CundecinosWeb.Controllers
{
    [Authorize]
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
            var model = new vSetupUsers()
            {
                Users = _context.People.Include(x => x.CollegeCareer).Include(x => x.Extension).ToList(),
                CollegeCareers = new SelectList(_context.CollegeCareer.ToList(), "CollegeCareerId", "Name")
		    };
			 return View(model);
        }

        public IActionResult Report()
        {
            var model = _context.Publication.Include(x => x.Person).ToList();

            return View(model);
        }
    }
}
