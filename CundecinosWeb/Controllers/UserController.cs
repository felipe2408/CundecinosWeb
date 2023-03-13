using CundecinosWeb.Models;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace CundecinosWeb.Controllers
{
    public class UserController : Controller
    {
        public IActionResult PersonalInformation()
        {
            return View();
        }

        

        

        public IActionResult Register()
        {
            var person = new Person();

            var user = User.FindFirstValue(ClaimTypes.NameIdentifier);

            person.FirstName = User.FindFirstValue(ClaimTypes.Name);
            person.LastName = User.FindFirstValue(ClaimTypes.GivenName);
            person.Email = User.FindFirstValue(ClaimTypes.Email);
            

            return View(person);
        }
        [HttpPost]

        public IActionResult Register(Person person)
        {

            return View();
        }

        [HttpPost]
        public IActionResult Registrer(Person person)
        {
            return RedirectToAction("SplashWelcomeCundecinos", "Splash");
        }
    }
}
