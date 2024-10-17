using Microsoft.AspNetCore.Mvc;

namespace OnlineQuiz.MVC.Controllers
{
    public class AdminController : Controller
    {
        public IActionResult DashBoard()
        {
            return View();
        }
    }
}
