using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.AzureADB2C.UI;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Mvc;

namespace CundecinosWeb.Controllers
{
	public class LoginController : Controller
	{
        [HttpGet]
        public IActionResult Logout()
        {
            var callbackUrl = Url.Action(nameof(HomeController.Index), "Home");
            return SignOut(
              new AuthenticationProperties { RedirectUri = callbackUrl },
              CookieAuthenticationDefaults.AuthenticationScheme,
              OpenIdConnectDefaults.AuthenticationScheme);
        }
    }
}
