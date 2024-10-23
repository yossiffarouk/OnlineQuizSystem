using OnlineQuiz.BLL.Dtos.Quiz;
using OnlineQuiz.DAL.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static OnlineQuiz.BLL.Dtos.Quiz.QuizDto;

namespace OnlineQuiz.BLL.Managers.Quiz
{
    public interface IQuizManager
    {
        IQueryable<QuizDto> GetAllQuizzes();
        IQueryable<QuizDto> GetAvailableQuizzes();
        QuizDto GetQuizById(int id);
        CreatQuizDTO AddQuiz(CreatQuizDTO quizDto);
        void UpdateQuiz(QuizDto quizDto);
        void DeleteQuiz(int id);
        IQueryable<QuizDto> GetQuizzesByTrackId(int trackId);
        IQueryable<FinalQuizDTO> GetQuizzesWithQuestionsAndOptions();
        FinalQuizDTO GetQuizByIdWithQuestions(int quizId);
        FinalQuizDTO GetQuizWithShuffledQuestions(int quizId);
        int AddQuizINI(CreatQuizDTO quizDto);
        IQueryable<Quizzes> GetAvailableQuizzesEnrolled(string studentId);
    }
}
