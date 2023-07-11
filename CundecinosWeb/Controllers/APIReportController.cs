using CundecinosWeb.Data;
using CundecinosWeb.Models;
using DevExtreme.AspNet.Data;
using DevExtreme.AspNet.Mvc;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

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
        [HttpGet]
        public async Task<List<PublicationComments>> GetOffers(Guid? ext)
        {
            if (ext == null)
            {
                var extension = _context.Extensions.FirstOrDefault().Name;
                return await _context.PublicationComments
                                        .Include(x=>x.Publication)
                                        .Include(x=>x.Person)
                                        .Include(x=>x.Person.Extension)
                                        .Where(x=>x.Person.Extension.Name == extension)
                                        .ToListAsync();
            }
            return await _context.PublicationComments
										.Include(x => x.Publication)
										.Include(x => x.Person)
										.Include(x => x.Person.Extension)
										.Where(x => x.Person.Extension.ExtensionId.Equals(ext))
										.ToListAsync();
		}

		[HttpGet]
		public async Task<IActionResult> Extensions(DataSourceLoadOptions loadOptions)
		{
			var lookup = from i in _context.Extensions
						 orderby i.Name
						 select new
						 {
							 Value = i.ExtensionId.ToString(),
							 Text = i.Name
						 };
			return Json(await DataSourceLoader.LoadAsync(lookup, loadOptions));
		}



	}
}
