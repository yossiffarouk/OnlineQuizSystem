using Microsoft.AspNetCore.Mvc;
using OnlineQuiz.BLL.Dtos.Options;
using OnlineQuiz.BLL.Dtos.Question;
using OnlineQuiz.BLL.Dtos.Quiz;
using OnlineQuiz.BLL.Dtos.Track;
using OnlineQuiz.BLL.Managers.Admin;
using OnlineQuiz.BLL.Managers.Instructor;
using OnlineQuiz.BLL.Managers.QuestionManager;
using OnlineQuiz.BLL.Managers.Quiz;
using OnlineQuiz.BLL.Managers.Student;
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
        private readonly IStudentManager _StudentManager;
        private readonly IInstructorManger _InstructorManger;

        public InstructorController(IAdminManger adminManger ,ITrackManager trackManager,IQuizManager quizManager,IQuestionManager questionManager ,QuizContext quizContext , IStudentManager studentManager ,IInstructorManger instructorManger)
        {
            _adminManger = adminManger;
            _trackManager = trackManager;
            _quizManager = quizManager;
            _questionManager = questionManager;
            _quizContext = quizContext;
            _StudentManager = studentManager;
            _InstructorManger = instructorManger;
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
                // Call the manager to add the quiz and get the quizId
                var quizId = _quizManager.AddQuizINI(quizDto);

                // Redirect to the QuizQuestion action with the created quizId
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
                QuizId = quizId,
                Options = new List<createOptionDto> // Initialize with 4 empty options
        {
            new createOptionDto(),
            new createOptionDto(),
            new createOptionDto(),
            new createOptionDto()
        }
            };

            return View("QuizQuestion", questionDto);
        }

        // POST: QuizQuestion
        [HttpPost("QuizQuestion")]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> create(createQuestionDto questionDto)
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
             var students = _StudentManager.GetAll();
            return View(students);
        }
        [HttpGet]
        [Route("Instructor/AddStudentToInstructor/{Id}")]
        public IActionResult AddStudentToInstructor(string Id)
        {
            var x = "8aa20076-192d-40e6-9e39-2714b2940214";
            _InstructorManger.AddStudentToInstructorAsync(Id, x);
            return RedirectToAction("GetStudents");

        }
        [Route("Instructor/MyStudents/{Id}")]
        public IActionResult MyStudents(string Id)
        {
            var students = _StudentManager.GetStudentsWithInstructor(Id);
            return View(students);

        }
        [HttpGet]
        [Route("Instructor/DeleteStudentFromInstructor/{Id}")]
        public IActionResult DeleteStudentFromInstructor(string Id)
        {
            var x = "8aa20076-192d-40e6-9e39-2714b2940214";
            _InstructorManger.RemoveStudentFromInstructorAsync(Id, x);
            return RedirectToAction("MyStudents", new { Id = "8aa20076-192d-40e6-9e39-2714b2940214" });

        }
    }
}
