using OnlineQuiz.DAL.Data.Models;
using OnlineQuiz.DAL.Repositoryies.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineQuiz.DAL.Repositoryies.AnswerRepository
{
    public interface IAnswerRepository : IRepository <Answers , int > 
    {
        string getcorrectanswer(Answers answer);
        List<Answers> GetAnswersByAttemptId(int attemptId);
        bool CheckCorrectAnswer(int questionId, string submittedAnswer);
        List<string> GetCorrectAnswersForQuiz(int quizId);
        string AnswerExist (int answerid);


    }
}
