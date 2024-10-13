using OnlineQuiz.BLL.Dtos.Attempt;
using OnlineQuiz.BLL.Dtos.Attempts;
using OnlineQuiz.BLL.Managers.Base;
using OnlineQuiz.DAL.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineQuiz.BLL.Managers.Attempt
{
    public interface IAttemptManager : IManager<AttemptDto,int>
    {
        List<QuesstionDto> StartQuizAttempt(StartQuizAttemptDto attempt);
        void SubmitQuizAttempt(int attemptId, List<AnswerDto> submittedAnswers);
        AttemptDto GetResults(int attemptId);
        IEnumerable<AttemptDto> GetUserAttempts(string studentId);
        StudentReadByIdDto GetStudentById(string studID);
        QuizReadByIdDto GetQuizById(int quizId);
        List<QuizScoreDto> GetTotalScoresByStudentId(string studentId);

    }
}
