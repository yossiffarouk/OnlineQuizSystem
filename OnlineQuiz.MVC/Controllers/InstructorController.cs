using Microsoft.AspNetCore.Mvc;
using OnlineQuiz.BLL.Managers.Admin;

namespace OnlineQuiz.MVC.Controllers
{
    public class InstructorController : Controller
    {
        private readonly IAdminManger _adminManger;

        public InstructorController(IAdminManger adminManger)
        {
            _adminManger = adminManger;
        }
        public IActionResult Dashboared()
        {
            var x = _adminManger.GetAllInstructo();
            return View(x);
        }
        public IActionResult QuizCreation()
        {

            return View("QuizCreation");
        }

    }
}
