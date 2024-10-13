using OnlineQuiz.DAL.Data.Models;
using OnlineQuiz.DAL.Repositoryies.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineQuiz.DAL.Repositoryies.StudentReposatory
{
    public interface IStudentRepo :IRepository<Student,string>
    {
        IQueryable<Student> GetStudentsByGrade(string grade);
        public Student GetByIdWithDetails(string studentId);
    }
}
