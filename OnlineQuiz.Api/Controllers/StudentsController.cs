using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OnlineQuiz.BLL.Dtos.StudentDtos;
using OnlineQuiz.BLL.Managers.Student;
using OnlineQuiz.DAL.Data.Models;

namespace OnlineQuiz.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentsController : ControllerBase
    {
        private readonly IStudentManager _studentManager;

        public StudentsController(IStudentManager studentManager)
        {
            _studentManager = studentManager;
        }
        // GET: api/Student
        [HttpGet]
        public ActionResult<IQueryable<StudentReadDto>> GetAllStudents()
        {
            var students = _studentManager.GetAll();
            return Ok(students);
        }
        // GET: api/Student/{id}
        [HttpGet("{id}")]
        public ActionResult<StudentReadDto> GetStudentById(string id)
        {
            var student = _studentManager.GetById(id);

            if (student == null)
            {
                return NotFound();
            }

            return Ok(student);
        }
        // GET: api/Student/Details/{id}
        [HttpGet("Details/{id}")]
        public ActionResult<StudentDetailesDto> GetStudentWithDetails(string id)
        {
            var student = _studentManager.GetByIdWithDetails(id);

            if (student == null)
            {
                return NotFound();
            }

            return Ok(student);
        }

        // GET: api/Student/Grade/{grade}
        [HttpGet("Grade/{grade}")]
        public ActionResult<IQueryable<StudentReadDto>> GetStudentsByGrade(string grade)
        {
            var students = _studentManager.GetStudentsByGrade(grade);
            if (students == null)
            {
                return NotFound();
            }

            return Ok(students);
        }
        // POST: api/Student
        [HttpPost]
        public ActionResult AddStudent(StudentAddDto studentAddDto)
        {
            _studentManager.Add(studentAddDto);
            return NoContent();
        }
        // PUT: api/Student/{id}
        [HttpPut("{id}")]
        public ActionResult UpdateStudent(string id, StudentUpdateDto studentUpdateDto)
        {
            if (id != studentUpdateDto.Id)
            {
                return BadRequest();
            }

            _studentManager.Update(studentUpdateDto);
            return NoContent();
        }
        [HttpGet("students")]
        public async Task<IActionResult> GetStudents(int pageNumber = 1, int pageSize = 10)
        {
            var paginatedStudents = await _studentManager.GetPaginatedStudentsAsync(pageNumber, pageSize);

            if (paginatedStudents == null)
            {
                return NotFound();
            }

            return Ok(paginatedStudents);
        }
        // DELETE: api/Student/{id}
        [HttpDelete("{id}")]
        public ActionResult DeleteStudent(string id)
        {
            var student = _studentManager.GetById(id);
            if (student == null)
            {
                return NotFound();
            }

            _studentManager.DeleteById(id);
            return NoContent();
        }



    }
}
