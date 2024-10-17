using Microsoft.AspNetCore.Mvc;
using OnlineQuiz.MVC.Models;
using System.Diagnostics;

namespace OnlineQuiz.MVC.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }



        public IActionResult Login()
        {

            return View("Login");
        }


        public IActionResult Register()
        {
            return View("Register");
        }
    }
}
