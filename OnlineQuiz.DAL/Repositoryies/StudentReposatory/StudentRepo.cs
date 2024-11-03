using Microsoft.EntityFrameworkCore;
using OnlineQuiz.DAL.Data.DBHelper;
using OnlineQuiz.DAL.Data.Models;
using OnlineQuiz.DAL.Repositoryies.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineQuiz.DAL.Repositoryies.StudentReposatory
{
    public class StudentRepo : Repository<Student,string> , IStudentRepo
    {
        private readonly QuizContext _quizContext;

        public StudentRepo(QuizContext quizContext):base(quizContext)
        {
            _quizContext = quizContext;
        }

        public  IQueryable<Student> GetStudentsByGrade(string grade)
        {
            return   _quizContext.Students
                                .Where(s => s.Grade == grade)
                                .AsQueryable();
        }

        public Student GetByIdWithDetails(string studentId)
        {
            return _quizContext.Students
               .Include(s => s.Attempts)
                   .ThenInclude(a => a.Quiz)
                    .Include(s => s.StudentInstructors)
                    .FirstOrDefault(s => s.Id == studentId);
        }

        public IEnumerable<StudentInstructor> GetStudentsWithInstructor(string InstructorId)
        {
            var x = _quizContext.StudentInstructors
                             .Include(si => si.Student) 
                             // Include the Student entity
                             .Where(si => si.InstructorId == InstructorId)
                          
                             .ToList();
            return x;
        }

        public IEnumerable<Student> GetStudentsToAdd(string instructorId)
        {
            var x = _quizContext.Students
        .Where(s => s.StudentInstructors.All(si => si.InstructorId != instructorId)).ToList();
            return x;
        }

        public Task<int> SaveChangesAsync()
        {
            
              return  _quizContext.SaveChangesAsync();
            
        }
    }
}
