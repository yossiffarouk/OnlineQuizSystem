using OnlineQuiz.DAL.Data.Models;
using OnlineQuiz.DAL.Repositoryies.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineQuiz.DAL.Repositoryies.QuizRepository
{
    public interface IQuizRepository : IRepository<Quizzes, int>
    {
        IQueryable<Quizzes> GetByTrackId(int trackId);
        IQueryable<Quizzes> GetAvailableQuizzes();
        IQueryable<Quizzes> GetQuizzesWithQuestions();
        Quizzes GetQuizByIdWithQuestions(int quizId);

    }
}
