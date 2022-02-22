using Microsoft.AspNetCore.Mvc;

namespace WebApp.Controllers
{
    public class MasterClassController : Controller
    {
        public IActionResult MasterClassList()
        {
            return View();
        }

        public IActionResult CreateMasterClass()
        {
            return View();
        }

        public IActionResult EditMasterClass()
        {
            return View();
        }
    }
}
