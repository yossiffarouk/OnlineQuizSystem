using Microsoft.AspNetCore.Mvc;
using OnlineQuiz.BLL.Dtos.Instructor.VM;
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
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;

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

            var instructorId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value; 
            var email = User.FindFirst(ClaimTypes.Email)?.Value; 
            var name = User.FindFirst(ClaimTypes.Name)?.Value; 


            if (string.IsNullOrEmpty(instructorId))
            {
                return Unauthorized(); 
            }

            var model = new InstructorDashboardVM
            {
                Id = instructorId,
                Email = email,
                Name = name
            };

            return View(model); 
        }
        [HttpGet]
        public IActionResult QuizCreation(string instructorId)
        {

            List<TrackDto> tracks = _trackManager.GetAll().ToList();


            ViewBag.Tracks = tracks;
            ViewBag.instructorid = instructorId;
       
            return View();
        }
        
        [ValidateAntiForgeryToken]
        [HttpPost]
        public IActionResult QuizCreation(CreatQuizDTO quizDto , string instructorId)
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToList();
                // Log the errors or return to the view with error messages
            }
            var tracks = _trackManager.GetAll().ToList();
            ViewBag.Tracks = tracks;

            if (ModelState.IsValid)
            {
                // Call the manager to add the quiz and get the quizId
                var quizId = _quizManager.AddQuizINI(quizDto);

                // Redirect to the QuizQuestion action with the created quizId
                return RedirectToAction("QuizQuestion", new { quizId, instructorId });
                
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

        public async Task<IActionResult> Create(createQuestionDto questionDto, string action)
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
        [HttpGet]
        public IActionResult QuizOfInstructor()
        {
            var instructorId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var quizzes = _quizManager.GetQuizzesByInstructorId(instructorId);
            return View(quizzes);
        }
    }
}
