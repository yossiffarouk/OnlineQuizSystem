using OnlineQuiz.BLL.Dtos.Admin.InstructorDtos;
using OnlineQuiz.BLL.Dtos.Instructor;
using OnlineQuiz.DAL.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineQuiz.BLL.Managers.Instructor
{
    public interface IInstructorManger
    {

        public string UpdateInstructorProfile(string id , UpdateInstructorrProfileDto UpdateInstructorrProfileDto);

        //add and delete in from instructor
        Task AddStudentToInstructorAsync(string studentId, string instructorId);
        Task RemoveStudentFromInstructorAsync(string studentId, string instructorId);

        // show scores
        IEnumerable<InstructorSeeQuizDto> ShowScoresOfQuiz(int quizId);
        InstructorReadDto GetInsById(string id);
    }
}
