using Microsoft.AspNetCore.Mvc;

namespace OnlineQuiz.MVC.Controllers
{
    public class StudentController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }


        public IActionResult AttemptQuiz()
        {
            return View();
        }
    }
}
