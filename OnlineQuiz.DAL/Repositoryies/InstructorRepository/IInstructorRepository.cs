using Microsoft.AspNet.Identity;
using OnlineQuiz.DAL.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineQuiz.DAL.Repositoryies.InstructorRepository
{
    public interface IInstructorRepository
    {



        // pls edit this 
        //Task<Instructor> GetInstructorByIdAsync(string userId);
        //Task UpdateInstructorProfileAsync(Instructor Instructor);

        Instructor GetInstructorById(string id);
        void UpdateInstructorProfile(Instructor Instructor);

        Task AddStudentToInstructorAsync(string studentId, string instructorId); // add student from instructor
        Task RemoveStudentFromInstructorAsync(string studentId, string instructorId); // Remove student from instructor


        // method get attemmpts to show student id and score scerch by quiz

        IEnumerable<Attempts> showAttempts(int quiz);
        Instructor GetInsById(string id);
        // save in database

        public IEnumerable<Student> GetAllStudentForQuiz(int QuizId);
        void savechanges();
    }
}
