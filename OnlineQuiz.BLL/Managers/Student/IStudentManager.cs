using OnlineQuiz.BLL.Dtos.StudentDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineQuiz.BLL.Managers.Student
{
    public interface IStudentManager 
    {
        IEnumerable<StudentReadDto> GetAll();
        StudentReadDto GetById(string id);
        void Add(StudentAddDto Studententity);
        void Update(StudentUpdateDto Studententity);
        void DeleteById(string id);
        IEnumerable<StudentReadDto> GetStudentsByGrade(string grade);
        public StudentDetailesDto GetByIdWithDetails(string studentId);

    }
}
