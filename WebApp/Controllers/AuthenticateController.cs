using Microsoft.AspNetCore.Mvc;

namespace WebApp.Controllers
{
    public class AuthenticateController : Controller
    {
        public IActionResult Register()
        {
            return View();
        }
        public IActionResult Login()
        {
            return View();
        }
    }
}