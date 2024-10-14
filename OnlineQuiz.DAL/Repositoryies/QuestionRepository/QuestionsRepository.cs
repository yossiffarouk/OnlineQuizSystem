using Microsoft.EntityFrameworkCore;
using OnlineQuiz.DAL.Data.DBHelper;
using OnlineQuiz.DAL.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineQuiz.DAL.Repositoryies.QuestionRepository
{
    public class QuestionsRepository : IQuestionsRepository
    {
        private readonly QuizContext _context;

        public QuestionsRepository(QuizContext context)
        {
            _context = context;
        }

        public IQueryable<Questions> GetAll()
        {
            return _context.Set<Questions>().Where(q => !q.IsDeleted).AsQueryable();
        }

        public Questions GetById(int id)
        {
            return _context.Set<Questions>()
                   .Include(q => q.Options)
                    .FirstOrDefault(q => q.Id == id && !q.IsDeleted);
        }

        public void Add(Questions entity)
        {
            _context.Set<Questions>().Add(entity);
            _context.SaveChanges();
        }

        public void Update(Questions entity)
        {
           
                _context.Set<Questions>().Update(entity);
                _context.SaveChanges();
            
        }

        public void DeleteById(int id)
        {
            var question = GetById(id);
            if (question != null)
            {
                
                question.IsDeleted = true; //soft delete
                _context.SaveChanges();
            }
        }
    }
}
