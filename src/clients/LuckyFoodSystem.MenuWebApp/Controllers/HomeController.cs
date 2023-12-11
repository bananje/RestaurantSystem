using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace RestaurantSystem.MenuWebApp.Controllers
{
    [Route("[controller]")]
    public class HomeController : Controller
    {
        [Route("[action]")]
        public IActionResult Index()
        {
            return View();
        }

        [Route("[action]")]
        public async Task<IActionResult> Logout()
        {
            return SignOut(CookieAuthenticationDefaults.AuthenticationScheme,
                           OpenIdConnectDefaults.AuthenticationScheme);
        }

        [Authorize]
        [Route("[action]")]
        public IActionResult Login()
        {
            return RedirectToAction(nameof(Index), "Home");
        }       
    }
}