using AutoMapper;
using OnlineQuiz.BLL.Dtos.Answer;
using OnlineQuiz.BLL.Dtos.Track;
using OnlineQuiz.DAL.Data.DBHelper;
using OnlineQuiz.DAL.Data.Models;
using OnlineQuiz.DAL.Repositoryies.AnswerRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineQuiz.BLL.Managers.Answer
{
    public class AnswersManager : IAnswersManager
    {
        private readonly IAnswerRepository _answerRepository;
        private readonly IMapper _mapper;

        public AnswersManager (IAnswerRepository IanswerRepository,IMapper mapper)
        {
            _answerRepository = IanswerRepository;
            _mapper = mapper; 
        }
        public void SubmitAnswer(int attemptId, int questionId, string submittedAnswer)
        {
            var isCorrect = _answerRepository.CheckCorrectAnswer(questionId, submittedAnswer);
            var answer = new Answers()
            {
                AttemptId = attemptId,
                QuestionId = questionId,
                SubmittedAnswer = submittedAnswer,
                IsCorrect = isCorrect
            };

            _answerRepository.Add(answer);
        }

        public List<Answers> GetUserAnswers(int attemptId)
        {
            return _answerRepository.GetAnswersByAttemptId(attemptId);
        }

        public List<string> GetCorrectAnswers(int quizId)
        {
            return _answerRepository.GetCorrectAnswersForQuiz(quizId);
        }
        List<Answers> IAnswersManager.GetUserAnswers(int attemptId)
        {

            return _answerRepository.GetAnswersByAttemptId(attemptId);
        }
        public string AnswerExist (int answerId)
        {
            return _answerRepository.AnswerExist(answerId);
        }


        public void Add(AnswerDto AnswerDto)
        {
            var answer = _mapper.Map<Answers>(AnswerDto);
            _answerRepository.Add(answer);

        }

        public void correctanswer(CorrectAnswerDto answer )
        {
            var x = _mapper.Map<Answers>(answer);
            if (answer.SubmittedAnswer == _answerRepository.getcorrectanswer(x))
            {
                answer.IsCorrect= true;
            }
        }
       

        public void correctanswer(AnswerDto answer)
        {
            throw new NotImplementedException();
        }
        public IQueryable<AnswerDto> GetAll()
        {
            throw new NotImplementedException();
        }
        public AnswerDto GetById(int id)
        {
            throw new NotImplementedException();
        }
        public void Update(AnswerDto entity)
        {
            throw new NotImplementedException();
        }
        public void DeleteById(int id)
        {
            throw new NotImplementedException();
        }
    }
}
