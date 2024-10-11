using AutoMapper;
using OnlineQuiz.BLL.Dtos.Attempt;
using OnlineQuiz.BLL.Dtos.Track;
using OnlineQuiz.DAL.Data.Models;
using OnlineQuiz.DAL.Repositoryies.AttemptRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineQuiz.BLL.Managers.Attempt
{
    public class AttemptManager : IAttemptManager
    {
        private readonly IAttemptRepository _attempt;
        
        public AttemptManager(IAttemptRepository attempt, IMapper mapper)
        {
            _attempt = attempt;
           
        }

        public void Add(AttemptDto entity)
        {
            var attempt = new Attempts
            {
                QuizId = entity.QuizId,
                StartTime = entity.StartTime,
                EndTime =(DateTime) entity.EndTime,
                Score = entity.Score,
                StudentId = entity.StudentId,
                StateForExam = entity.stateforexam
            };

            _attempt.Add(attempt);
        }
        public void StartQuizAttempt(StartQuizAttemptDto dto)
        {
            Quizzes quiz = _attempt.GetQuizById(dto.QuizId);
            var student = _attempt.GetStudentById(dto.StudentId);

            if (quiz == null)
            {
                throw new Exception("Quiz not found");
            }
            else if (student == null)
            {
                throw new Exception("Student not found");
            }

            Attempts attempt = new Attempts
            {
                QuizId = dto.QuizId,
                StartTime = DateTime.Now,
                EndTime = DateTime.Now.AddHours(2),
                StateForExam = "Started",
                StudentId = dto.StudentId,
            };

            _attempt.StartQuizAttempt(attempt);
        }

        public void DeleteById(int id)
        {
            _attempt.DeleteById(id);
        }

        public IQueryable<AttemptDto> GetAll()
        {
            var attempts = _attempt.GetAll();

            return attempts.Select(attempt => new AttemptDto
            {
                QuizId = attempt.QuizId,
                StartTime = attempt.StartTime,
                EndTime = attempt.EndTime,
                Score = attempt.Score,
                StudentId = attempt.StudentId,
                stateforexam = attempt.StateForExam,
                
            }).AsQueryable();
        }

        public AttemptDto GetById(int id)
        {
            var attempt = _attempt.GetById(id);
            if (attempt == null) throw new Exception("Attempt not found");

           
            return new AttemptDto
            {
                QuizId = attempt.QuizId,
                StartTime = attempt.StartTime,
                EndTime = attempt.EndTime,
                Score = attempt.Score,
                StudentId = attempt.StudentId,
                stateforexam = attempt.StateForExam,
                
            };
        }

        public QuizReadByIdDto GetQuizById(int quizId)
        {
            var quiz = _attempt.GetQuizById(quizId);
            if (quiz == null)
            {
                throw new Exception("Quiz not found");
            }

            
            return new QuizReadByIdDto
            {
                Id = quiz.Id,
                Title = quiz.Tittle,
                Description = quiz.Description
            };
        }

        public AttemptDto GetResults(int attemptId)
        {

            var attempt = _attempt.GetResults(attemptId);
            if (attempt == null)
                throw new Exception("Attempt not found");


            return new AttemptDto
            {
                Id = attempt.Id,
                QuizId = attempt.QuizId,
                StudentId = attempt.StudentId,
                Score = attempt.Score,
                StartTime = attempt.StartTime,
                EndTime = attempt.EndTime,
               stateforexam = attempt.StateForExam
                
                
                
            };
        }

        public StudentReadByIdDto GetStudentById(string studID)
        {
            var student = _attempt.GetStudentById(studID);
            if (student == null)
            {
                throw new Exception("Student not found");
            }

            
            return new StudentReadByIdDto
            {
                Id = student.Id,
                Name = student.UserName
            };
        }

        public IEnumerable<AttemptDto> GetUserAttempts(string studentId)
        {

            var attempts = _attempt.GetUserAttempts(studentId);

            if (attempts == null || !attempts.Any())
                throw new Exception("No attempts found for the user");

            return attempts.Select(attempt => new AttemptDto
            {
                Id = attempt.Id,
                QuizId = attempt.QuizId, 
                StudentId = attempt.StudentId,
                StartTime = attempt.StartTime,
                EndTime = attempt.EndTime,
                Score = attempt.Score,
                stateforexam= attempt.StateForExam
                
            }).ToList();
        }

      

        public void SubmitQuizAttempt(int attemptId, List<AnswerDto> submittedAnswers)
        {
            var answers = submittedAnswers.Select(a => new Answers
            {
                QuestionId = a.queid,
                SubmittedAnswer = a.SubmittedAnswer,
                

            }).ToList();

            _attempt.SubmitQuizAttempt(attemptId, answers);
        }


        public void Update(AttemptDto entity)
        {
            throw new NotImplementedException();
        }
    }
}
