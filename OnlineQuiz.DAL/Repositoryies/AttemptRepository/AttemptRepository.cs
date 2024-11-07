using OnlineQuiz.DAL.Data.DBHelper;
using OnlineQuiz.DAL.Data.Models;
using OnlineQuiz.DAL.Repositoryies.Base;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineQuiz.DAL.Repositoryies.AttemptRepository
{
    public class AttemptRepository : IRepository<Attempts, int>, IAttemptRepository
    {
        private readonly QuizContext _context;

        public AttemptRepository(QuizContext context)
        {
            _context = context;
        }

        public void StartQuizAttempt(Attempts attempt)
        {
            _context.attempts.Add(attempt);
            _context.SaveChanges();
        }

        public void DeleteById(int id)
        {
            Attempts? attempts = _context.attempts.FirstOrDefault(a => a.Id == id);
            if (attempts != null)
            {
                _context.attempts.Remove(attempts!);
                _context.SaveChanges();
            }
            throw new Exception("Not Found");
        }

        public IQueryable<Attempts> GetAll()
        {
            return _context.attempts;
        }

        public Attempts GetById(int id)
        {
            Attempts? attempts = _context.attempts.FirstOrDefault(a => a.Id == id);
            return attempts!;
        }

        public void Add(Attempts attempt)
        {
            throw new NotImplementedException();
        }

        public void Update(Attempts entity)
        {
            _context.SaveChanges();
        }

        public void SubmitQuizAttempt(int attemptId, List<Answers> submittedAnswers)
        {
            Attempts? attempt = _context.attempts.FirstOrDefault(a => a.Id == attemptId);


            if (attempt == null && attempt!.EndTime > DateTime.Now)
                throw new Exception("Attempt not found");

            foreach (Answers answer in submittedAnswers)
            {

                Answers answers = new Answers
                {
                    AttemptId = attemptId,
                    QuestionId = answer.QuestionId,
                    SubmittedAnswer = answer.SubmittedAnswer,
                    IsCorrect = _context.questions.FirstOrDefault(q => q.Id == answer.QuestionId)
                    ?.CorrectAnswer == answer.SubmittedAnswer
                };

                
                if (answers.IsCorrect == true)
                {
                    attempt.Score = attempt.Score +1;
                }
                _context.answers.Add(answers);
            }
            

            attempt.EndTime = DateTime.Now;
            _context.SaveChanges();
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }

        public Attempts GetResults(int Id)
        {
            Attempts? attempt = _context.attempts.FirstOrDefault(a => a.Id == Id);

            if (attempt == null)
                throw new Exception("Attempt not found");

            return attempt!;
        }

        public IEnumerable<Attempts> GetUserAttempts(string studentId)
        {
            IEnumerable<Attempts> attempts = _context.attempts.Where(a => a.StudentId == studentId).ToList();
            if (attempts != null)
            {
                return attempts;
            }
            else
            {
                throw new Exception("Attempt not found");
            }

        }
        private double CalculateScore(int attemptId)
        {
            int correctAnswers = _context.answers
                .Where(a => a.AttemptId == attemptId && a.IsCorrect)
                .Count();

            int totalQuestions = _context.answers
                .Where(a => a.AttemptId == attemptId)
                .Count();

            if (totalQuestions == 0)
            {
                return 0;
            }

            return (double)correctAnswers / totalQuestions * 100;
        }

        public int TotalAnswers(int attemptId)
        {
            int totalQuestions = _context.answers
                .Where(a => a.AttemptId == attemptId)
                .Count();
            return totalQuestions;
        }
        public int CorrectAnswers(int attemptId) 
        {
            int correctAnswers = _context.answers
                .Where(a => a.AttemptId == attemptId && a.IsCorrect)
                .Count();
            return correctAnswers;
        }
        public int WrongAnsers(int attemptId) 
        {
            int wronganswers = _context.answers
                .Where(a => a.AttemptId == attemptId && !a.IsCorrect)
                .Count();
            return wronganswers;
        }
        public Student GetStudentById(string id)
        {
            Student? student = _context.Users.OfType<Student>().FirstOrDefault(s => s.Id == id);
            return student!;
        }
        public string GetGrade(int totalScore)
        {
            
            if (totalScore < 50)
            {
                return "Failed";
            }
            else if (totalScore >= 50 && totalScore < 65)
            {
                return "Fair";
            }
            else if (totalScore >= 65 && totalScore < 75)
            {
                return "Good";
            }
            else if (totalScore >= 75 && totalScore < 85)
            {
                return "Very Good";
            }
            else
            {
                return "Excellent";
            }
        }


        public Quizzes GetQuizById(int quizID)
        {
            Quizzes? quizzes = _context.quizzes.FirstOrDefault(q => q.Id == quizID);
            return quizzes!;
        }

        public IEnumerable<Questions> questions(int quizId)
        {
            return _context.questions.Where(q => q.QuizId == quizId).Include(q => q.Options).ToList();
        }

        public IEnumerable<Attempts> GetAttemptsByStudentId(string studentId)
        {
            return _context.attempts
                  .Where(a => a.StudentId == studentId)
                  .Include(a => a.Quiz)
                  .ToList();
        }
    }
}
