using AutoMapper;
using OnlineQuiz.BLL.Dtos.Attempt;
using OnlineQuiz.BLL.Dtos.Attempts;
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
                EndTime = (DateTime)entity.EndTime,
                Score = entity.Score,
                StudentId = entity.StudentId,
                StateForExam = entity.stateforexam
            };

            _attempt.Add(attempt);
        }
        public List<QuesstionDto> StartQuizAttempt(StartQuizAttemptDto dto)
        {
            Quizzes quiz = _attempt.GetQuizById(dto.QuizId);
            OnlineQuiz.DAL.Data.Models.Student student = _attempt.GetStudentById(dto.StudentId);

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
            int attemptid = attempt.Id;
            return GetQuestionsByQuizId(quiz.Id , attemptid);
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
            IEnumerable<Questions> ques = _attempt.questions(quiz.Id).ToList();
            if (quiz == null)
            {
                throw new Exception("Quiz not found");
            }

            var questionDtos = ques.Select(q => new QuesstionDto
            {
                Id = q.Id,
                Tittle = q.Tittle
            }).ToList();


            return new QuizReadByIdDto
            {
                Id = quiz.Id,
                Title = quiz.Tittle,
                Description = quiz.Description,

            };
        }

        public GetResultAttemptDto GetResults(int attemptId)
        {
            Attempts? attempt = _attempt.GetResults(attemptId);
            if (attempt == null)
                throw new Exception("Attempt not found");

            return new GetResultAttemptDto
            {
                QuizName = attempt.Quiz.Tittle.ToString(),
                StudentName = attempt.Student.UserName,
                TotalAnswers = _attempt.TotalAnswers(attemptId),
                CorrectAnswers = _attempt.CorrectAnswers(attemptId),
                WrongAnswers = _attempt.WrongAnsers(attemptId),
                Score = (int)attempt.Score
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

        public IEnumerable<GetUserAttemptsDto> GetUserAttempts(string studentId)
        {

            var attempts = _attempt.GetUserAttempts(studentId);

            if (attempts == null || !attempts.Any())
                throw new Exception("No attempts found for the user");

            return attempts.Select(attempt => new GetUserAttemptsDto
            {
                AttemptID = attempt.Id,
                QuizName = attempt.Quiz.Tittle,
                StudentName = attempt.Student.UserName,
                StartTime = attempt.StartTime,
                EndTime = attempt.EndTime,
                Score = (int)attempt.Score,

            }).ToList();
        }

        public void SubmitQuizAttempt(int attemptId, List<AnswerDto> submittedAnswers)
        {
            List<Answers> answers = submittedAnswers.Select(a => new Answers
            {
                QuestionId = a.QuestionId,
                SubmittedAnswer = a.SubmittedAnswer,


            }).ToList();

            _attempt.SubmitQuizAttempt(attemptId, answers);
            //_attempt.SaveChanges();
        }

        public void Update(AttemptDto entity)
        {
            Attempts attempts = _attempt.GetById(entity.Id);
            if (attempts == null)
            {
                throw new Exception("Not Found");
            }
            attempts.StartTime = entity.StartTime;
            attempts!.EndTime = (DateTime)entity.EndTime;
            attempts.Score = entity.Score;
            attempts.StateForExam = entity.stateforexam;
            _attempt.Update(attempts);
        }

        public List<QuesstionDto> GetQuestionsByQuizId(int quizId, int attemptid)
        {
            IEnumerable<Questions> questions = _attempt.questions(quizId).ToList();
            if (questions != null)
            {

                List<QuesstionDto> questionDtos = new List<QuesstionDto>();



                foreach (var question in questions)
                {
                    questionDtos.Add(new QuesstionDto
                    {
                        Id = question.Id,
                        Tittle = question.Tittle,
                        options = question.Options.Select(option => new OptionsDto
                        {
                            Text = option.OptionText
                        }).ToList(),
                        attemptid = attemptid
                    });
                }
                return questionDtos;
            }
            else
            {
                throw new Exception("Student not found");
            }
        }

        public List<QuizScoreDto> GetTotalScoresByStudentId(string studentId)
        {
            var attempts = _attempt.GetAttemptsByStudentId(studentId);

            if (attempts == null || !attempts.Any())
            {
                throw new Exception("No attempts found for this student.");
            }

            var quizScores = attempts
                .GroupBy(a => a.QuizId)
                .Select(group => new QuizScoreDto
                {
                    QuizId = group.Key,
                    QuizTitle = group.First().Quiz.Tittle,
                    TotalScore = group.Sum(a => a.Score),
                    Grade = _attempt.GetGrade(group.Sum(a => (int)a.Score))
                })
                .ToList();

            return quizScores;
        }
    }
}
