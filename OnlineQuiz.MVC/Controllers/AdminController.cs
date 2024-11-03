using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OnlineQuiz.BLL.Dtos.Accounts;
using OnlineQuiz.BLL.Dtos.Admin.InstructorDtos;
using OnlineQuiz.BLL.Dtos.Admin.Roles;
using OnlineQuiz.BLL.Dtos.Admin.Share;
using OnlineQuiz.BLL.Dtos.Admin.StudentDtos;
using OnlineQuiz.BLL.Managers.Accounts;
using OnlineQuiz.BLL.Managers.Admin;
using OnlineQuiz.BLL.Managers.Quiz;
using OnlineQuiz.BLL.ViewModels.Admin;
using OnlineQuiz.DAL.Data.Models;
using System.IO;
using System.Security.Claims;

namespace OnlineQuiz.MVC.Controllers
{
    public class AdminController : Controller
    {
        private readonly IAdminManger _manger;
        private readonly IQuizManager _quizManager;
        private readonly IAccountManager _accountManager;

        public IMapper _mapper { get; }

        public AdminController(IAdminManger manger, IQuizManager quizManager , IMapper mapper ,
            IAccountManager accountManager)
        {
           _manger = manger;
            _quizManager = quizManager;
            _mapper = mapper;
            _accountManager = accountManager;
        }
        // get dashbored and show static for admin

        [Authorize(Roles = Roles.Admin)]
        public IActionResult DashBoard()
        {
            if (!User.Identity.IsAuthenticated)
            {
                // Log or handle unauthenticated access
                return RedirectToAction("Login", "Home");
            }
            SharedStatics statics = new SharedStatics()
            {
                NumOfInstructor = _manger.NumOfInstructor(),
                NumOfStudent = _manger.NumOfStudent(),
                NumOfAttempes = _manger.NumOfAttempes(),
                NumOfQuiz = _manger.NumOfQuizes()
            };
            return View(statics);
        }




        //-----------------------------------------------------------Mangage Roles--------------------------------------------------------------------------------------------
        [Authorize(Roles = Roles.Admin)]
        public IActionResult ManageRoles()
        {
            return View("ManageRoles");
        }


        [Authorize(Roles = Roles.Admin)]
        [HttpGet]
        public IActionResult AddRole()
        {
            return View(new AddRoleVm());
        }
        [Authorize(Roles = Roles.Admin)]
        [HttpPost]
        public async Task<IActionResult> AddRole(AddRoleVm model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var response = await _accountManager.AddRole(model.RoleName);

            if (response.successed)
            {

                TempData["SuccessMessage"] = "Role added successfully!";
                return RedirectToAction("AddRole");
            }


            foreach (var error in response.Errors)
            {
                ModelState.AddModelError(string.Empty, error);
            }

            return View(model);
        }

        [Authorize(Roles = Roles.Admin)]
        [HttpGet]
        public IActionResult DeleteRole()
        {
            return View(new DeleteOrRestoreRole());
        }

        [Authorize(Roles = Roles.Admin)]
        [HttpPost]
        public async Task<IActionResult> DeleteRole(DeleteOrRestoreRole model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var response = await _accountManager.DeleteRole(model.RoleId);

            if (response.successed)
            {

                TempData["SuccessMessage"] = "Role deleted successfully!";
                return RedirectToAction("DeleteRole");
            }


            foreach (var error in response.Errors)
            {
                ModelState.AddModelError(string.Empty, error);
            }

            return View(model);
        }

        [Authorize(Roles = Roles.Admin)]
        [HttpGet]
        public IActionResult RestoreRole()
        {
            var model = new DeleteOrRestoreRole();
            return View(model);
        }

        [Authorize(Roles = Roles.Admin)]
        [HttpPost]
        public async Task<IActionResult> RestoreRole(DeleteOrRestoreRole model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var response = await _accountManager.RestoreRole(model.RoleId);

            if (response.successed)
            {

                TempData["SuccessMessage"] = "Role restored successfully!";
                return RedirectToAction("RestoreRole");
            }

            foreach (var error in response.Errors)
            {
                ModelState.AddModelError(string.Empty, error);
            }

            return View(model);
        }

        [Authorize(Roles = Roles.Admin)]
        [HttpGet]
        public IActionResult AddRoleToUser()
        {
            var model = new AddRoleToUser();
            return View(model);
        }

        [Authorize(Roles = Roles.Admin)]
        [HttpPost]
        public async Task<IActionResult> AddRoleToUser(AddRoleToUser model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var response = await _accountManager.AddRoleToUser(model.UserId, model.RoleId);

            if (response.successed)
            {
                TempData["SuccessMessage"] = "Role added to user successfully!";
                return RedirectToAction("AddRoleToUser");
            }

            // Add any errors returned by the manager to the model state
            foreach (var error in response.Errors)
            {
                ModelState.AddModelError(string.Empty, error);
            }

            return View(model);
        }

        [Authorize(Roles = Roles.Admin)]
        [HttpGet]
        public IActionResult DeleteRoleFromUser()
        {
            var model = new AddRoleToUser();
            return View(model);
        }

        [Authorize(Roles = Roles.Admin)]
        [HttpPost]
        public async Task<IActionResult> DeleteRoleFromUser(AddRoleToUser model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var response = await _accountManager.RemoveRoleFromUser(model.UserId, model.RoleId);

            if (response.successed)
            {
                TempData["SuccessMessage"] = "Role Deleted from user successfully!";
                return RedirectToAction("AddRoleToUser");
            }

            // Add any errors returned by the manager to the model state
            foreach (var error in response.Errors)
            {
                ModelState.AddModelError(string.Empty, error);
            }

            return View(model);
        }

        [Authorize(Roles = Roles.Admin)]
        [HttpGet]
        public async Task<IActionResult> GetRoles()
        {

            var roles = await _accountManager.GetAllRoles();

            return View(roles);
        }

        [Authorize(Roles = Roles.Admin)]
        [HttpGet]
        public async Task<IActionResult> GetDeletedRoles()
        {

            var roles = await _accountManager.GetAllRolesIsDeleted();

            return View(roles);
        }

        [Authorize(Roles = Roles.Admin)]
        [HttpGet]
        public async Task<IActionResult> GetUsersInRole()
        {
            // Fetch all roles to populate the roles list
            var roles = await _accountManager.GetAllRoles();

            var response = new RoleResponce<UserRoleInfo>
            {
                Data = new UserRoleInfo
                {
                    Roles = roles.Select(r => new GetAllRolesDto
                    {
                        RoleId = r.RoleId,
                        RoleName = r.RoleName
                    }).ToList()
                }
            };

            return View(response);
        }

        [Authorize(Roles = Roles.Admin)]
        [HttpPost]
        public async Task<IActionResult> GetUsersInRole(string roleId)
        {
            // Fetch all roles to populate the roles list for the dropdown
            var roles = await _accountManager.GetAllRoles();

            var response = new RoleResponce<UserRoleInfo>
            {
                Data = new UserRoleInfo
                {
                    Roles = roles.Select(r => new GetAllRolesDto
                    {
                        RoleId = r.RoleId,
                        RoleName = r.RoleName
                    }).ToList()
                }
            };

            var result = await _accountManager.GetUsersInRole(roleId);

            if (!result.successed)
            {
                response.Errors = result.Errors;
                return View(response);
            }

            // Set successful data
            response.successed = true;
            response.Data.UsersCount = result.Data.UsersCount;
            response.Data.RoleName = result.Data.RoleName;
            response.Data.Users = result.Data.Users;

            return View(response);
        }



        //-------------------------------------------------------------------------------------------------------------------------------------------------------


        // get ins by id 
        [HttpGet]
        [Authorize(Roles = Roles.Instructor)]
        public IActionResult Profile()
        {
            //var = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            // Fetch the student ID from the claims (after login)
            // string studentId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var instructorId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (string.IsNullOrEmpty(instructorId))
            {
                return NotFound(); // Handle missing student ID
            }

            // Fetch student details including quiz attempts and instructors
            var InsDetails = _manger.GetInstructorById(instructorId);

            if (InsDetails == null)
            {
                return NotFound(); // Handle if the student is not found
            }

            return View(InsDetails); // Pass the student data to the view

        }






        // get all student and instructor section ------------------------------------
        [Authorize(Roles = Roles.Admin)]
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
        [Authorize(Roles = Roles.Admin)]
        [HttpPost]
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

        [Authorize(Roles = Roles.Admin)]
        [HttpPost]
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
        [Authorize(Roles = Roles.Admin)]
        [Route("Admin/DeleteStudent/{id}")]
        public IActionResult DeleteStudent(string id)
        {

            _manger.DeleteStudent(id);
            return RedirectToAction("GetAllStudents");
        }


        // delete instructor
        [Authorize(Roles = Roles.Admin)]
        [Route("Admin/DeleteInstructor/{id}")]
        public IActionResult DeleteInstructor(string id)
        {

            _manger.DeleteInstructor(id);
            return RedirectToAction("GetAllInstructors");
        }

        //  instructor panding --------------------------------------------------------
        [Authorize(Roles = Roles.Admin)]
        [HttpGet]
        [Route("Admin/NewInstructors")]
        public IActionResult NewInstructors()
        {
          var pandininstructors =  _manger.GetAllInstructorPanding();

            return View(pandininstructors);
        }

        // approve and deny methodes for instructors --------------------------------
        [Authorize(Roles = Roles.Admin)]
        [HttpGet]
        [Route("Admin/Approve/{id}")]
        public IActionResult Approve(string id)
        {

            _manger.ApproveInstructorAsync(id);
            return RedirectToAction("NewInstructors");
        }

        [Authorize(Roles = Roles.Admin)]
        [HttpGet]
        [Route("Admin/Deny/{id}")]
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
        [Authorize(Roles = Roles.Admin)]
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

        [HttpGet]
        [Authorize(Roles = "Instructor,Admin")]
        public IActionResult EditIns()
        {
            // Fetch instructorId from the current logged-in user
            var instructorId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(instructorId))
            {
                return NotFound("Instructor ID is missing.");
            }

            // Fetch instructor details for edit
            var instructorReadDto = _manger.GetInstructorById(instructorId);

            if (instructorReadDto == null)
            {
                return NotFound("Instructor not found.");
            }

            var instructorUpdateDto = _mapper.Map<IstrurctorUpdateDto>(instructorReadDto);
            return View("EditIns", instructorUpdateDto); // Pass the DTO to the view
        }


        [HttpPost]
        [Authorize(Roles = "Instructor,Admin")]
        [Route("Admin/EditInstructor")]
        public IActionResult EditInstructor(  IstrurctorUpdateDto IstrurctorUpdateDto)
        {
            string id = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (id== IstrurctorUpdateDto.Id)
            {
                _manger.UpdateInstructor(IstrurctorUpdateDto);
                return RedirectToAction("Profile", "Instructor", new { area = "Instructor" });
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
