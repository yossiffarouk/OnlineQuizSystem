using Microsoft.AspNetCore.Mvc;
using OnlineQuiz.BLL.Managers.Quiz;

namespace OnlineQuiz.MVC.Controllers
{
    public class StudentController : Controller
    {
        private readonly IQuizManager _quizManager;

        public StudentController(IQuizManager quizManager)
        {
            _quizManager = quizManager;
        }
        public IActionResult Index()
        {
            return View();
        }


  
        public IActionResult AttemptQuiz()
        {
            var availableQuizzes = _quizManager.GetAvailableQuizzes().ToList();

            return View(availableQuizzes);
        }
        public IActionResult GetQuestions()
        {
            return View();
        }

        public IActionResult PastQuizzes()
        {
            return View();
        }
    }
}
