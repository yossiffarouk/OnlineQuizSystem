using Microsoft.AspNet.Identity;
using Microsoft.EntityFrameworkCore;
using OnlineQuiz.DAL.Data.DBHelper;
using OnlineQuiz.DAL.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineQuiz.DAL.Repositoryies.InstructorRepository
{
    public class InstructorRepository : IInstructorRepository
    {
        private readonly QuizContext _context;

        public InstructorRepository(QuizContext context)
        {
            _context = context;
        }

       


        //---------------------------
        // Add student to instructor
        public async Task AddStudentToInstructorAsync(string studentId, string instructorId)
        {
            var student = await _context.Students.FindAsync(studentId);
            var instructor = await _context.Instructors.FindAsync(instructorId);

            if (student == null || instructor == null)
                throw new Exception("Student or Instructor not found");

            var studentInstructor = new StudentInstructor
            {
                StudentId = studentId,
                InstructorId = instructorId
            };

            _context.StudentInstructors.Add(studentInstructor);
            await _context.SaveChangesAsync();
        }

        // Remove student from instructor
        public async Task RemoveStudentFromInstructorAsync(string studentId, string instructorId)
        {
            var studentInstructor = await _context.StudentInstructors
                .FirstOrDefaultAsync(si => si.StudentId == studentId && si.InstructorId == instructorId);

            if (studentInstructor == null)
                throw new Exception("The relationship does not exist");

            _context.StudentInstructors.Remove(studentInstructor);
            await _context.SaveChangesAsync();
        }




        // ------------------------------------------------------------------------------


        //get score(attempts) of quiz by id
        public IEnumerable<Attempts> showAttempts(int quiz)
        {

            return _context.attempts.Where(x => x.QuizId == quiz).OrderBy(x=>x.Score);
        }



        public Instructor GetInstructorById(string id)
        {
           return _context.Instructors.FirstOrDefault(x => x.Id == id);
        }

        public void UpdateInstructorProfile(Instructor Instructor)
        {
            _context.Instructors.Update(Instructor);
            savechanges();
        }




        // ------------------------------------------------------------------------------
        public void savechanges()
        {
            _context.SaveChanges();
        }

  
    }
}
