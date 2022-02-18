using Microsoft.AspNetCore.Mvc;
using LIbrary.DTOs;

namespace WebApp.Controllers
{
    public class StateController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Create()
        {
            return View();
        }
        public IActionResult CreateUsingModel()
        {
            return View(new AddStateModel());
        }
        public IActionResult UsingRepo()
        {
            return View();
        }

        public IActionResult ForStateGet()
        {
            return View();
        }

        public IActionResult ForStatePut()
        {
            return View();
        }

        public IActionResult ForStatePost()
        {
            return View();
        }
    }
}
