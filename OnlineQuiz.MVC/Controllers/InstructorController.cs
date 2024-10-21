using Microsoft.AspNetCore.Mvc;
using OnlineQuiz.BLL.Dtos.Question;
using OnlineQuiz.BLL.Dtos.Quiz;
using OnlineQuiz.BLL.Dtos.Track;
using OnlineQuiz.BLL.Managers.Admin;
using OnlineQuiz.BLL.Managers.QuestionManager;
using OnlineQuiz.BLL.Managers.Quiz;
using OnlineQuiz.BLL.Managers.Track;
using OnlineQuiz.DAL.Data.DBHelper;
using OnlineQuiz.DAL.Data.Models;

namespace OnlineQuiz.MVC.Controllers
{
    public class InstructorController : Controller
    {
        private readonly IAdminManger _adminManger;
        private readonly ITrackManager _trackManager;
        private readonly IQuizManager _quizManager;
        private readonly IQuestionManager _questionManager;
        private readonly QuizContext _quizContext;

        public InstructorController(IAdminManger adminManger ,ITrackManager trackManager,IQuizManager quizManager,IQuestionManager questionManager ,QuizContext quizContext)
        {
            _adminManger = adminManger;
            _trackManager = trackManager;
            _quizManager = quizManager;
            _questionManager = questionManager;
            _quizContext = quizContext;
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
                
                var quizId = _quizContext.quizzes
                           .OrderByDescending(q => q.Id)
                           .Select(q => q.Id)
                            .FirstOrDefault(); 
               
                return RedirectToAction("QuizQuestion", new { quizId });
            }
            return View(quizDto);
        }



        // GET: QuizQuestion/{quizId}
        [HttpGet("QuizQuestion/{quizId}")]
        public async Task<IActionResult> QuizQuestion(int quizId)
        {
            
            var questionDto = new createQuestionDto
            {
                QuizId = quizId 
            };

          
            return View("QuizQuestion", questionDto); 
        }

        // POST: QuizQuestion
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> QuizQuestion(createQuestionDto questionDto)
        {
            if (ModelState.IsValid)
            {
               
                await _questionManager.AddQuestionAsync(questionDto);

              
                return RedirectToAction("QuizQuestion", new { quizId = questionDto.QuizId });
            }

            
            return View("QuizQuestion", questionDto); 
        }

        public IActionResult GetStudents()
        {

            return View();
        }

    }
}
