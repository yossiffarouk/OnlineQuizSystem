using OnlineQuiz.BLL.Dtos.Answer;
using OnlineQuiz.BLL.Dtos.Attempt;
using OnlineQuiz.BLL.Dtos.Quiz;
using OnlineQuiz.BLL.Managers.Base;
using OnlineQuiz.DAL.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineQuiz.BLL.Managers.Answer
{
    public interface IAnswersManager : IManager <AnswerDto, int >
    {
        void SubmitAnswer(int attemptId, int questionId, string submittedAnswer);
        List<Answers> GetUserAnswers(int attemptId);
        List<string> GetCorrectAnswers(int quizId);
        string AnswerExist(int answerid);
        void correctanswer(AnswerDto answer);
        public ResultDto GetResult(List<AnswerDto> answers, FinalQuizDTO quiz, GetResultAttemptDto score);
    }

}
