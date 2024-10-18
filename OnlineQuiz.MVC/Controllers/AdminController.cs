using Microsoft.AspNetCore.Mvc;
using OnlineQuiz.BLL.Managers.Admin;

namespace OnlineQuiz.MVC.Controllers
{
    public class AdminController : Controller
    {
        private readonly IAdminManger _manger;

        public AdminController(IAdminManger manger)
        {
           _manger = manger;
        }
        public IActionResult DashBoard()
        {
            //statics
            return View();
        }

        public IActionResult GetAllStudents()
        {
            //students
            var students = _manger.GetAllStudentAsync();
            return View(students);
        }
        public IActionResult GetAllInstructors()
        {
            //Instructors
            var Instructors = _manger.GetAllInstructo();
            return View(Instructors);
        }
        public IActionResult BanUser()
        {
            //Instructors
             //_manger.BanUserAsync();
            return View();
        }

    }
}
