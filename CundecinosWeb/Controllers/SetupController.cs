using CundecinosWeb.Data;
using CundecinosWeb.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

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
            ViewBag.CollegeCareers = _context.CollegeCareer.ToList();
            ViewBag.Extensions = _context.Extensions.ToList();


            return View();
        }

        public IActionResult Category()
        {


            return View();
        }

        public IActionResult ReportPeople()
        {
			var model = new vSetupUsers()
			{
				Users = _context.People.Include(x => x.CollegeCareer).Include(x => x.Califications).Include(x => x.Extension).ToList(),
				CollegeCareers = new SelectList(_context.CollegeCareer.ToList(), "CollegeCareerId", "Name")
			};
			return View(model);
		}

    }
}
