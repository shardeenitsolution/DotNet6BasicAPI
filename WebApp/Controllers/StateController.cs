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
        public IActionResult Edit()
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
    }
}
