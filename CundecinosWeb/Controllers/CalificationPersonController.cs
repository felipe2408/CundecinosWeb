using CundecinosWeb.Data;
using CundecinosWeb.Models;
using CundecinosWeb.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CundecinosWeb.Controllers
{
    public class CalificationPersonController : Controller
    {

        private readonly DataContext _context;
        public CalificationPersonController(DataContext context) {
        _context = context;
        
        }   

        public IActionResult Create(Guid id)
        {

            var publication = _context.Publication.Include(x => x.Person).Where(x => x.PublicationID == id).FirstOrDefault();

            var vModel = new vCalificationPerson();

            vModel.Person = publication.Person;
            vModel.Publication = publication;



            var calification = new CalificationPerson();
            calification.PersonID = publication.PersonID;


            vModel.CalificationPerson = calification;

            return View(vModel);
        }

        [HttpPost]
        public IActionResult Create(vCalificationPerson calification)
        {

            var model = calification.CalificationPerson;
            
            model.CalificationDate = DateTime.Now;

            _context.Add(model);
            _context.SaveChanges();

            return RedirectToAction("MyOffersAccept","InofferPublication");
        }

    }
}
