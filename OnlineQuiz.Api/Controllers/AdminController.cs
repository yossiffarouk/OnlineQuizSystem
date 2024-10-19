using Azure.Messaging;
using Microsoft.AspNet.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using OnlineQuiz.BLL.Dtos.Admin.InstructorDtos;
using OnlineQuiz.BLL.Dtos.Admin.StudentDtos;
using OnlineQuiz.BLL.Managers.Admin;

namespace OnlineQuiz.Api.Controllers
{
    [Route("api/[controller]")]
    //[ApiController]
    //[Authorize(Roles ="Admin")]
    public class AdminController : ControllerBase
    {
        private readonly IAdminManger _iAdminManger;

   

        public AdminController(IAdminManger IAdminManger )
        {
            _iAdminManger = IAdminManger;

        }

        [HttpGet]
        [Route("GetAllStudents")]
        public async Task <IActionResult> GetAllStudents()
        {
            var students =  await _iAdminManger.GetAllStudentAsync();
            return Ok( students );
        }

        [HttpGet]
        [Route("GetStudentById/{id}")]
        public IActionResult GetStudentById(string id)
        {
            return Ok(_iAdminManger.GetStudentById(id));
        }


        [HttpGet]
        [Route("GetStudentByName/{Name}")]
        public IActionResult GetStudentByName(string Name)
        {
            return Ok(_iAdminManger.GetStudentByName(Name));
        }


        [HttpPost]
        [Route("AddStudent")]

        public IActionResult AddStudent(StudentAddDto studentAddDto)
        {
            _iAdminManger.AddStudent(studentAddDto);
            return Ok(new {message = "Student Added succefuly"});
        }

        [HttpDelete]
        [Route("DeleteStudent/{id}")]
        public IActionResult DeleteStudent(string id) 
        {
            _iAdminManger.DeleteStudent(id);
            return Ok(new { message = "Student Deleted succefuly" });
        }

        [HttpPut]
        [Route("UpdateStudent/{id}")]
        public IActionResult UpdateStudent(string id , StudentUpdateDto StudentUpdateDto)
        {

            _iAdminManger.UpdateStudent(StudentUpdateDto);
            return Ok(new { message = "Student updated succefuly" });
        }


        //---------------------------------------------------------------------------------
        [HttpGet]
        [Route("GetAllInstructor")]
        public IActionResult GetAllInstructor()
        {
            return Ok(_iAdminManger.GetAllInstructo());
        }

        [HttpGet]
        [Route("GetInstructorById/{id}")]
        public IActionResult GetInstructorById(string id)
        {
            return Ok(_iAdminManger.GetInstructorById(id));
        }

        [HttpGet]
        [Route("GetInstructorByName/{Name}")]
        public IActionResult GetInstructorByName(string Name)
        {
            return Ok(_iAdminManger.GetInstructorByName(Name));
        }


        [HttpPost]
        [Route("AddInstructor")]

        public IActionResult AddInstructor(string id ,InstructorAddDto InstructorAddDto)
        {
            _iAdminManger.AddInstructor(InstructorAddDto);
            return Ok(new { message = "Instructor Added succefuly" });
        }

        [HttpDelete]
        [Route("DeleteInstructor/{id}")]
        public IActionResult DeleteInstructor(string id)
        {
            _iAdminManger.DeleteInstructor(id);
            return Ok(new { message = "Instructor Deleted succefuly" });
        }

        [HttpPut]
        [Route("UpdateInstructor/{id}")]
        public IActionResult UpdateInstructor(string id, IstrurctorUpdateDto IstrurctorUpdateDto)
        {

            _iAdminManger.UpdateInstructor(IstrurctorUpdateDto);
            return Ok(new { message = "Instructor updated succefuly" });
        }
        //-------------------------------------------------------------------------------------------
        [HttpGet]
        [Route("BanUser/{id}")]
        public IActionResult BanUser(string id)
        {

            _iAdminManger.BanUserAsync(id);
            return Ok(new { message = " user ban succefully " });
        }
        [HttpGet]
        [Route("UnBanUser/{id}")]
        public IActionResult UnBanUser(string id)
        {

            _iAdminManger.UnbanUserAsync(id);
            return Ok(new { message = " user unban succefully " });
        }


        [HttpGet]
        [Route("ApproveIns/{id}")]
        public IActionResult ApproveIns(string id)
        {

            var x =_iAdminManger.ApproveInstructorAsync(id);
            return Ok(x);
        }
        [HttpGet]
        [Route("DenyIns/{id}")]
        public IActionResult DenyIns(string id)
        {

            var x =  _iAdminManger.DenyInstructorAsync(id);
            return Ok( x);
        }

        //---------------------------

        [HttpGet]
        [Route("GetNumOfStudent")]
        public IActionResult GetNumOfStudent()
        {
            return Ok(_iAdminManger.NumOfStudent());
        }


        [HttpGet]
        [Route("GetNumOfInstructor")]
        public IActionResult GetNumOfInstructor()
        {
            return Ok(_iAdminManger.NumOfInstructor());
        }


        [HttpGet]
        [Route("GetNumOfQuzies")]
        public IActionResult GetNumOfQuzies()
        {
            return Ok(_iAdminManger.NumOfQuizes());
        }


        [HttpGet]
        [Route("GetNumOfAttempes")]
        public IActionResult GetNumOfAttempes()
        {
            return Ok(_iAdminManger.NumOfAttempes());
        }

        

    }
}
