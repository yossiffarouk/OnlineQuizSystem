using OnlineQuiz.DAL.Data.Models;
using OnlineQuiz.DAL.Repositoryies.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineQuiz.DAL.Repositoryies.AttemptRepository
{
    public interface IAttemptRepository : IRepository<Attempts, int>
    {
        void StartQuizAttempt(Attempts attempt);
        void SubmitQuizAttempt(int attemptId, List<Answers> submittedAnswers);
        Attempts GetResults(int attemptId);
        IEnumerable<Attempts> GetUserAttempts(string studentId);
        Student GetStudentById(string studID);
        Quizzes GetQuizById(int quizId);
    }
}
