using Microsoft.AspNetCore.Mvc;

namespace CundecinosWeb.Controllers
{
    public class InofferPublicationController : Controller
    {

        //Mis Ofertas Recibidas
        public IActionResult OffersReceived()
        {
            return View();
        }

        //Mis ofertas a las publicaciones
        public IActionResult Offers()
        {
            return View();
        }




    }
}
