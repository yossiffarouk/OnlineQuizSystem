using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using OnlineQuiz.BLL.Managers.Attempt;
using OnlineQuiz.BLL.Managers.Quiz;
using OnlineQuiz.BLL.Managers.Student;
using OnlineQuiz.BLL.AutoMapper.StudentMapper;
using OnlineQuiz.MVC.Models;
using System.Security.Claims;
using OnlineQuiz.BLL.Dtos.StudentDtos;
using OnlineQuiz.DAL.Data.Models;


namespace OnlineQuiz.MVC.Controllers
{
    public class StudentController : Controller
    {
        private readonly IQuizManager _quizManager;
        private readonly IAttemptManager _attemptManager;
        private readonly IStudentManager _studentManager;
        private readonly IMapper _mapper;

        public StudentController(IQuizManager quizManager,IAttemptManager attemptManager,IStudentManager studentManager,IMapper mapper)
        {
            _quizManager = quizManager;
            _attemptManager = attemptManager;
            _studentManager = studentManager;
            _mapper = mapper;
        }
        public IActionResult Index()
        {
            return View();
        }
       
        public IActionResult Profile()
        {

            string studentId = "bbcd3d4d-21b3-4fed-962f-aaacab9db911";

            if (string.IsNullOrEmpty(studentId))
            {
                return NotFound(); // Handle missing student ID
            }

            // Fetch student details including quiz attempts and instructors
            var studentDetails = _studentManager.GetByIdWithDetails(studentId);

            if (studentDetails == null)
            {
                return NotFound(); // Handle if the student is not found
            }

            return View(studentDetails); // Pass the student data to the view
        }


        public IActionResult MyInstractors()
        {

            string studentId = "bbcd3d4d-21b3-4fed-962f-aaacab9db911";
            if (string.IsNullOrEmpty(studentId))
            {
                return NotFound(); // Handle missing student ID
            }

            // Fetch student details including quiz attempts and instructors
            var studentDetails = _studentManager.GetByIdWithDetails(studentId);

            if (studentDetails == null)
            {
                return NotFound(); // Handle if the student is not found
            }

            return View(studentDetails); // Pass the student data to the view
        }


        public IActionResult MyQuizzes()
        {
            string studentId = "bbcd3d4d-21b3-4fed-962f-aaacab9db911";

            if (string.IsNullOrEmpty(studentId))
            {
                return NotFound(); // Handle missing student ID
            }

            // Fetch student details including quiz attempts and instructors
            var studentDetails = _studentManager.GetByIdWithDetails(studentId);

            if (studentDetails == null)
            {
                return NotFound(); // Handle if the student is not found
            }

            return View(studentDetails); // Pass the student data to the view
        }


        // Fetch student details for editing
        [HttpGet]
        public IActionResult Edit()
        {

            string studentId = "bbcd3d4d-21b3-4fed-962f-aaacab9db911";
            if (string.IsNullOrEmpty(studentId))
            {
                return NotFound("Student ID is missing.");
            }

            // Fetch student details for edit
            var studentReaddto = _studentManager.GetById(studentId);

            if (studentReaddto == null)
            {
                return NotFound("Student not found.");
            }

            var studentUpdateDto = _mapper.Map<StudentUpdateDto>(studentReaddto);
            return View("Edit", studentUpdateDto); // Pass the DTO to the view
        }

        // Handle post-back from the form after edit
        [HttpPost]
        public IActionResult Edit(StudentUpdateDto studentUpdateDto)
        {
            if (!ModelState.IsValid)
            {
                // Return form with validation errors
                return View(studentUpdateDto);
            }

            try
            {
                // Update the student details
                _studentManager.Update(studentUpdateDto);

                // Redirect to the Profile page after successful update
                return RedirectToAction("Profile");
            }
            catch (Exception ex)
            {
                // Log the error (consider using a logging framework like Serilog)
                ModelState.AddModelError("", "Error updating student information.");
                return View(studentUpdateDto);
            }
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
        



















        //public IActionResult Index()
        //{

        //    // Fetch the student ID from the claims (after login)
        //    string studentId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;


        //    if (string.IsNullOrEmpty(studentId))
        //    {
        //        return NotFound(); // Handle missing student ID
        //    }

        //    // Fetch student details including quiz attempts and instructors
        //    var studentDetails = _studentManager.GetByIdWithDetails(studentId);

        //    if (studentDetails == null)
        //    {
        //        return NotFound(); // Handle if the student is not found
        //    }

        //    return View(studentDetails); // Pass the student data to the view

        //}





        //public IActionResult Index(string studentId)
        //{
        //    // Retrieve student details including attempts and instructors
        //    var studentDetails = _studentManager.GetByIdWithDetails(studentId);

        //    // Check if the student exists
        //    if (studentDetails == null)
        //    {
        //        return NotFound();
        //    }

        //    // Pass data to the view
        //    return View(studentDetails);
        //}
        /*
        public IActionResult Dashboard()
        {
            var studentId = User.Claims.FirstOrDefault(c => c.Type == "studentId")?.Value;
            var completedQuizzes = _quizManager.GetAllQuizzes(studentId);
            var averageScore = _attemptManager.GetResults(studentId);

            var viewModel = new StudentDashboardViewModel
            {
                CompletedQuizzes = completedQuizzes,
                AverageScore = averageScore
            };

            return View(viewModel);
        }
        
        */
        //public IActionResult Profile() 
        //    {
        //    return View();


        // }
        //public IActionResult Profile()
        //{
        //    string studentId = TempData["StudentId"] as string; // Retrieve the student ID

        //    if (string.IsNullOrEmpty(studentId))
        //    {
        //        return NotFound(); // Handle missing student ID
        //    }

        //    // Fetch student details including quiz attempts and instructors
        //    var studentDetails = _studentManager.GetByIdWithDetails(studentId);

        //    if (studentDetails == null)
        //    {
        //        return NotFound(); // Handle if the student is not found
        //    }

        //    return View(studentDetails); // Pass the student data to the view
        //}


    

