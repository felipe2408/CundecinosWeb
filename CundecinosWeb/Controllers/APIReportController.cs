using CundecinosWeb.Data;
using CundecinosWeb.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CundecinosWeb.Controllers
{
	public class APIReportController : Controller
	{
		private readonly DataContext _context;

		public APIReportController(DataContext context)
		{
			_context = context;

		}

		[HttpGet]
		public async Task<IActionResult> ReportPublication()
		{
			return View();
		}

		[HttpGet]

        //public async Task<List<Publication>> PublicationReport(Guid identity)
        //{


        //          var publications = _context.Publication.Include(x => x.Person).Include(x => x.Person.CollegeCareer).Include(x => x.Person.Extension).Where(x=>x.Person.UID == identity).ToList();

        //	return (publications);

        //      }
        //      [Route("/[controller]/PublicationReportUser")]
        //      [HttpGet]

        //      public async Task<List<Publication>> PublicationReportUser(Guid identity)
        public async Task<List<Publication>> PublicationReport()
        {


            var publications = _context.Publication.Include(x => x.Person).Include(x => x.Person.CollegeCareer).Include(x => x.Person.Extension).ToList();

            return (publications);

        }
        [Route("/[controller]/PublicationReportUser")]
        [HttpGet]

        public async Task<List<Publication>> PublicationReport(Guid identity)
        {


            var publications = _context.Publication.Include(x => x.Person).Include(x => x.Person.CollegeCareer).Include(x => x.Person.Extension).Where(x => x.Person.UID == identity).ToList();
            return (publications);

        }
        [HttpGet]

        public async Task<List<Extension>> ExtensionReport()
        {

			var extensions = _context.Extensions.ToList();
            return (extensions);

        }

        [HttpGet]

        public async Task<List<CollegeCareer>> CollegeCareerReport()
        {

            var collegeCareers = _context.CollegeCareer.ToList();
            return (collegeCareers);

        }
        


    }
}
