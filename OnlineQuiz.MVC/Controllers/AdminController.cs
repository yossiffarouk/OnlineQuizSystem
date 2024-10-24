using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OnlineQuiz.BLL.Dtos.Accounts;
using OnlineQuiz.BLL.Dtos.Admin.InstructorDtos;
using OnlineQuiz.BLL.Dtos.Admin.Roles;
using OnlineQuiz.BLL.Dtos.Admin.Share;
using OnlineQuiz.BLL.Dtos.Admin.StudentDtos;
using OnlineQuiz.BLL.Managers.Admin;
using OnlineQuiz.BLL.Managers.Quiz;
using OnlineQuiz.BLL.ViewModels.Admin;

namespace OnlineQuiz.MVC.Controllers
{
    public class AdminController : Controller
    {
        private readonly IAdminManger _manger;
        private readonly IQuizManager _quizManager;

        public IMapper _mapper { get; }

        public AdminController(IAdminManger manger, IQuizManager quizManager , IMapper mapper)
        {
           _manger = manger;
            _quizManager = quizManager;
            _mapper = mapper;
        }
        // get dashbored and show static for admin
        [Authorize(Roles = Roles.Admin)]
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
        [Authorize(Roles = Roles.Admin)]
        public IActionResult ManageRoles()
        {
            return View("ManageRoles");
        }


        [Authorize(Roles = Roles.Admin)]
        public IActionResult AddRole()
        {
            return View("AddRole");
        }
        [Authorize(Roles = Roles.Admin)]
        public IActionResult DeleteRole()
        {
            var model = new DeleteOrRestoreRole();
            return View(model);
        }
        [Authorize(Roles = Roles.Admin)]
        public IActionResult RestoreRole()
        {
            var model = new DeleteOrRestoreRole();
            return View(model);
        }
        [Authorize(Roles = Roles.Admin)]
        public IActionResult AddRoleToUser()
        {
            var model = new AddRoleToUser();
            return View(model);
        }
        [Authorize(Roles = Roles.Admin)]
        public IActionResult DeleteRoleFromUser()
        {
            var model = new AddRoleToUser();
            return View(model);
        }









        [Authorize(Roles = Roles.Admin)]
        // get all student and instructor section ------------------------------------
        public IActionResult GetAllStudents()
        {
            //students
            var students = _manger.GetAllStudentAsync();
            return View(students);
        }
        [Authorize(Roles = Roles.Admin)]
        public IActionResult GetAllInstructors()
        {
            //Instructors
            var Instructors = _manger.GetAllInstructo();
            return View(Instructors);
        }

        // ban and unban ------------------------
        [HttpPost]
        [Authorize(Roles = Roles.Admin)]
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

        [HttpPost]
        [Authorize(Roles = Roles.Admin)]
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
        [Authorize(Roles = Roles.Admin)]
        public IActionResult DeleteStudent(string id)
        {

            _manger.DeleteStudent(id);
            return RedirectToAction("GetAllStudents");
        }
        // delete instructor
       
        [Route("Admin/DeleteInstructor/{id}")]
        [Authorize(Roles = Roles.Admin)]
        public IActionResult DeleteInstructor(string id)
        {

            _manger.DeleteInstructor(id);
            return RedirectToAction("GetAllInstructors");
        }

        //  instructor panding --------------------------------------------------------
        [HttpGet]
        [Route("Admin/NewInstructors")]
        [Authorize(Roles = Roles.Admin)]
        public IActionResult NewInstructors()
        {
          var pandininstructors =  _manger.GetAllInstructorPanding();

            return View(pandininstructors);
        }
        // approve and deny methodes for instructors --------------------------------
        [HttpGet]
        [Route("Admin/Approve/{id}")]
        [Authorize(Roles = Roles.Admin)]
        public IActionResult Approve(string id)
        {

            _manger.ApproveInstructorAsync(id);
            return RedirectToAction("NewInstructors");
        }
        [HttpGet]
        [Route("Admin/Deny/{id}")]
        [Authorize(Roles = Roles.Admin)]
        public IActionResult Deny(string id)
        {

             _manger.DenyInstructorAsync(id);
            return RedirectToAction("NewInstructors");
        }

        // add section ------------------------

        [HttpGet]
        [Authorize(Roles = Roles.Admin)]
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
        [Authorize(Roles = Roles.Admin)]
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
        [Authorize(Roles = Roles.Admin)]
        public IActionResult Edit(string id , string page)
        {if (page == "InstructorPage")
            
            {
                var x = _manger.GetInstructorByIdForUpdate(id);
                return View("EditInstructor",x); 
            }
        else 
            {
               var x = _manger.GetStudentByIdForUpdate(id);
                return View("EditStudent",x);

            }   
        }

        [HttpPost]
        [Route("Admin/EditInstructor/{id}")]
        [Authorize(Roles = Roles.Admin)]
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
        [Route("Admin/EditStudent/{id}")]
        [Authorize(Roles = Roles.Admin)]
        public IActionResult EditStudent(string id, StudentUpdateDto StudentUpdateDto)
        {

            _manger.UpdateStudent(StudentUpdateDto);
            return RedirectToAction("GetAllStudents");
        }

        [HttpGet]
        [Route("Admin/GetAllQuizzes")]
        [Authorize(Roles = Roles.Admin)]
        public IActionResult GetAllQuizzes()
        {

            var x =_quizManager.GetAllQuizzes();
            return View(x);
        }
        [HttpPost]
        [Route("Admin/DeleteQuiz/{id}")]
        [Authorize(Roles = Roles.Admin)]
        public IActionResult DeleteQuiz(int id)
        {

             _quizManager.DeleteQuiz(id);
            return RedirectToAction("GetAllQuizzes");
        }
    }
}
