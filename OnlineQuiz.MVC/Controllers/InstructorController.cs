using Microsoft.AspNetCore.Mvc;
using OnlineQuiz.BLL.Dtos.Quiz;
using OnlineQuiz.BLL.Dtos.Track;
using OnlineQuiz.BLL.Managers.Admin;
using OnlineQuiz.BLL.Managers.Quiz;
using OnlineQuiz.BLL.Managers.Track;
using OnlineQuiz.DAL.Data.Models;

namespace OnlineQuiz.MVC.Controllers
{
    public class InstructorController : Controller
    {
        private readonly IAdminManger _adminManger;
        private readonly ITrackManager _trackManager;
        private readonly IQuizManager _quizManager;

        public InstructorController(IAdminManger adminManger ,ITrackManager trackManager,IQuizManager quizManager)
        {
            _adminManger = adminManger;
            _trackManager = trackManager;
            _quizManager = quizManager;
        }
        public IActionResult Dashboared()
        {
            //var x = _adminManger.GetAllInstructo();
            return View();
        }
        [HttpGet]
        public IActionResult QuizCreation()
        {

            List<TrackDto> tracks = _trackManager.GetAll().ToList();

           
            ViewBag.Tracks = tracks;

            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken] 
        public IActionResult QuizCreation(CreatQuizDTO quizDto)
        {
            var tracks = _trackManager.GetAll().ToList(); 
            ViewBag.Tracks = tracks;
            if (ModelState.IsValid)
            {
                _quizManager.AddQuiz(quizDto);
                return RedirectToAction("QuizQuestion"); 
            }

            
        

            return View(quizDto); 
        }

        public IActionResult QuizQuestion()
        {

            return View();
        }

        public IActionResult GetStudents()
        {

            return View();
        }

    }
}
