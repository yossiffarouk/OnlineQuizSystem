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

        public  IEnumerable<Student> GetStudentsByGrade(string grade)
        {
            return  _quizContext.Students
                                .Where(s => s.Grade == grade)
                                .AsNoTracking()
                                .ToList();
        }

        public Student GetByIdWithDetails(string studentId)
        {
            return _quizContext.Students
                .Include(s => s.Attempts)             // Include attempts
                .ThenInclude(a => a.Quiz)         // Include quizzes for each attempt
                .FirstOrDefault(s => s.Id == studentId);
        }
    }
}
