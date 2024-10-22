using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using OnlineQuiz.BLL.Dtos.Admin.InstructorDtos;
using OnlineQuiz.BLL.Dtos.Admin.Share;
using OnlineQuiz.BLL.Dtos.Admin.StudentDtos;
using OnlineQuiz.BLL.Managers.Admin;
using OnlineQuiz.BLL.ViewModels.Admin;

namespace OnlineQuiz.MVC.Controllers
{
    public class AdminController : Controller
    {
        private readonly IAdminManger _manger;

        public IMapper _mapper { get; }

        public AdminController(IAdminManger manger , IMapper mapper)
        {
           _manger = manger;
            _mapper = mapper;
        }
        // get dashbored and show static for admin
        public IActionResult DashBoard()
        {
            SharedStatics statics = new SharedStatics()
            {
                NumOfInstructor = _manger.NumOfInstructor(),
                NumOfStudent = _manger.NumOfStudent(),
                NumOfAttempes = _manger.NumOfAttempes(),
                NumOfQuiz = _manger.NumOfQuizes()
            };
            return View(statics);
        }
        // get all student and instructor section ------------------------------------
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

        // ban and unban ------------------------
        [HttpGet]
        public IActionResult Ban(string id,string page)
        {
          
           _manger.BanUserAsync(id);
            if (page == "StudentPage") {
                return RedirectToAction("GetAllStudents");
            }
            else
            {
                return RedirectToAction("GetAllInstructors");
            }
        }

        [HttpGet]
        
        public IActionResult UnBan(string id, string page)
        {
            _manger.UnbanUserAsync(id);
            if (page == "StudentPage")
            {
                return RedirectToAction("GetAllStudents");
            }
            else
            {
                return RedirectToAction("GetAllInstructors");
            }
           
        }

        //------------------------------------------------------
        // delete student
        [Route("Admin/DeleteStudent/{id}")]
        public IActionResult DeleteStudent(string id)
        {

            _manger.DeleteStudent(id);
            return RedirectToAction("GetAllStudents");
        }
        // delete instructor
       
        [Route("Admin/DeleteInstructor/{id}")]
        public IActionResult DeleteInstructor(string id)
        {

            _manger.DeleteInstructor(id);
            return RedirectToAction("GetAllInstructors");
        }

        //  instructor panding --------------------------------------------------------
        [HttpGet]
        [Route("Admin/NewInstructors")]
        public IActionResult NewInstructors()
        {
          var pandininstructors =  _manger.GetAllInstructorPanding();

            return View(pandininstructors);
        }
        // approve and deny methodes for instructors --------------------------------
        [HttpGet]
        [Route("Admin/Approve/{id}")]
        public IActionResult Approve(string id)
        {

            var x = _manger.ApproveInstructorAsync(id);
            return Ok(x);
        }
        [HttpGet]
        [Route("Admin/Deny/{id}")]
        public IActionResult Deny(string id)
        {

            var x = _manger.DenyInstructorAsync(id);
            return Ok(x);
        }

        // add section ------------------------

        [HttpGet]
        public IActionResult Add(string sourcePage)
        {
            
            if (sourcePage == "InstructorPage")
            {
                return View("AddInstructor");
            }
            else
            {
                return View("AddStudent");
            }
        }
        [HttpPost]
        public IActionResult Add(InstructorAddDto InstructorAddDto)
        {
            _manger.AddInstructor(InstructorAddDto);
            return RedirectToAction("GetAllInstructors");
        }
        [HttpPost]
        public IActionResult AddStudent(StudentAddDto StudentAddDto)
        {
            _manger.AddStudent(StudentAddDto);
            return RedirectToAction("GetAllStudents");
        }

        ///edit section ---------------------------------------------------------------------------------


        [HttpGet]
        public IActionResult Edit(string id , string page)
        {if (page == "InstructorPage")
            
            {
                var x = _manger.GetInstructorByIdForUpdate(id);
                return View("EditInstructor",x); 
            }
        else 
            {
               
                return View("EditStudent");

            }   
        }

        [HttpPost]
        public IActionResult EditInstructor(string id , IstrurctorUpdateDto IstrurctorUpdateDto)
        {
            if (id== IstrurctorUpdateDto.Id)
            {
                _manger.UpdateInstructor(IstrurctorUpdateDto);
                return RedirectToAction("GetAllInstructors");
            }
           return NoContent();
        }
        [HttpPost]
        public IActionResult EditStudent(string id, StudentUpdateDto StudentUpdateDto)
        {

            _manger.UpdateStudent(StudentUpdateDto);
            return RedirectToAction("GetAllStudents");
        }

    }
}
