using Microsoft.AspNetCore.Mvc;

namespace WebApp.Controllers
{
    public class MasterCourseController : Controller
    {
        public IActionResult MasterCourseList()
        {
            return View();
        }

        public IActionResult CreateMasterCourse()
        {
            return View();
        }

        public IActionResult EditMasterCourse()
        {
            return View();
        }
    }
}
