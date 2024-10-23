using Microsoft.EntityFrameworkCore;
using OnlineQuiz.DAL.Data.DBHelper;
using OnlineQuiz.DAL.Data.Models;
using OnlineQuiz.DAL.Repositoryies.Base;
using System;
using System.Collections.Generic;

using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineQuiz.DAL.Repositoryies.QuizRepository
{
    public class QuizRepository :  IRepository<Quizzes, int>, IQuizRepository
    {
        private readonly QuizContext _context;

        public QuizRepository(QuizContext context)
        {
            _context = context;
        }

        public IQueryable<Quizzes> GetAll()
        {
            return _context.Set<Quizzes>().Where(q => !q.IsDeleted).AsQueryable();
        }

        public Quizzes GetById(int id)
        {
            return _context.Set<Quizzes>().FirstOrDefault(q => q.Id == id && !q.IsDeleted);
        }

        public void Add(Quizzes entity)
        {
            _context.Set<Quizzes>().Add(entity);
            _context.SaveChanges();
        }

        public void Update(Quizzes entity)
        {
            if (_context.Entry(entity).State == EntityState.Detached)
            {
                _context.quizzes.Attach(entity);
            }
            _context.Entry(entity).State = EntityState.Modified;
            _context.SaveChanges();
        }

            public void DeleteById(int id)
        {
            var quiz = _context.Set<Quizzes>().FirstOrDefault(q => q.Id == id);
            if (quiz != null)
            {
                quiz.IsDeleted = true; // Mark as soft-deleted
                _context.SaveChanges();
            }
        }

        public IQueryable<Quizzes> GetByTrackId(int trackId)
        {
            return _context.Set<Quizzes>().Where(q => q.TracksId == trackId && !q.IsDeleted);
        }

        public IQueryable<Quizzes> GetAvailableQuizzes()
        {
            return _context.Set<Quizzes>().Where(q => q.IsAvailable && !q.IsDeleted);
        }
        public IQueryable<Quizzes> GetQuizzesWithQuestions()
        {
            return _context.Set<Quizzes>()
                 .Where(q => !q.IsDeleted)
                .Include(q => q.Questions) 
                    .ThenInclude(q => q.Options) 
                .AsQueryable();
        }
        public Quizzes GetQuizByIdWithQuestions(int quizId)
        {
            return _context.quizzes
                .Include(q => q.Questions) 
                .ThenInclude(q => q.Options)
                 .FirstOrDefault(q => q.Id == quizId && !q.IsDeleted);
        }
        public int AddQuizINT(Quizzes entity)
        {
            _context.Set<Quizzes>().Add(entity);
            _context.SaveChanges();

            // After saving, return the quiz ID
            return entity.Id;
        }
        public IQueryable<Quizzes> GetAvailableQuizzesEnrolled(string studentId)
        {
            //// Get all available quizzes
            //var availableQuizzes = _context.Set<Quizzes>()
            //    .Where(q => q.IsAvailable && !q.IsDeleted);

            //// Get quizzes taught by instructors that the student is enrolled with
            //var enrolledQuizzes = from quiz in _context.Set<Quizzes>()
            //                      join si in _context.Set<StudentInstructor>()
            //                      on quiz.InstructorId equals si.InstructorId
            //                      where si.StudentId == studentId
            //                      select quiz;

            //// Combine both results
            //var result = availableQuizzes.Union(enrolledQuizzes);

            //return result;

            return _context.Set<Quizzes>()
        .Where(q => q.IsAvailable && !q.IsDeleted &&
                    q.Instructor.StudentInstructors.Any(si => si.StudentId == studentId)); // Filter quizzes where the student is enrolled
        //.Include(q => q.Instructor) // Include the Instructor entity
        //.ThenInclude(i => i.StudentInstructors) // Include the StudentInstructors entity
        //.ThenInclude(si => si.Student); // Include the Student entity
        }
    }
}
