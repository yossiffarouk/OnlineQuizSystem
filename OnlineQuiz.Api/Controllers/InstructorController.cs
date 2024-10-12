using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OnlineQuiz.BLL.Dtos.Instructor;
using OnlineQuiz.BLL.Managers.Instructor;
using System.Security.Claims;

namespace OnlineQuiz.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InstructorController : ControllerBase
    {
        private readonly IInstructorManger _iInstructorManger;

        public InstructorController(IInstructorManger IInstructorManger)
        {
            _iInstructorManger = IInstructorManger;
        }



        [HttpPut]
        [Route("UpdateUserProfile")]
         public IActionResult UpdateInstructorProfile(string instructorId, UpdateInstructorrProfileDto UpdateInstructorrProfileDto)
        {
            

     
            var result =  _iInstructorManger.UpdateInstructorProfile(instructorId, UpdateInstructorrProfileDto);


            return Ok(result);
        }


        [HttpGet]
        [Route("GetQuizScores")]
        public  IActionResult GetQuizScores(int quizId)
        {
            return Ok(_iInstructorManger.ShowScoresOfQuiz(quizId));
        }

        [HttpPost("{instructorId}/students/{studentId}")]
        public async Task<IActionResult> AddStudentToInstructor(string studentId, string instructorId)
        {
            try
            {
                await _iInstructorManger.AddStudentToInstructorAsync(studentId, instructorId);
                return Ok(new { message = "Student added to instructor successfully" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        // Endpoint to remove a student from an instructor
        [HttpDelete("{instructorId}/students/{studentId}")]
        public async Task<IActionResult> RemoveStudentFromInstructor(string studentId, string instructorId)
        {
            try
            {
                await _iInstructorManger.RemoveStudentFromInstructorAsync(studentId, instructorId);
                return Ok(new { message = "Student removed from instructor successfully" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }



    }
}
