using OnlineQuiz.DAL.Data.Models;
using OnlineQuiz.DAL.Repositoryies.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineQuiz.DAL.Repositoryies.QuestionRepository
{
    public interface IQuestionsRepository : IRepository<Questions, int>
    {
        // Additional methods can be defined here if needed
        void DeleteOptionById(int optionId);
        Option GetOptionById(int optionId);
        Task AddAsync(Questions entity);
    }
}
