﻿using OnlineQuiz.BLL.Dtos.Options;
using OnlineQuiz.BLL.Dtos.Question;
using OnlineQuiz.DAL.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineQuiz.BLL.Managers.QuestionManager
{
    public interface IQuestionManager
    {
        IQueryable<QuestionDto> GetAllQuestions();
        QuestionDto GetQuestionById(int id);
        void AddQuestion(createQuestionDto createquestionDto);
        void UpdateQuestion(QuestionDto questionDto);
        void DeleteQuestion(int id);
        void DeleteOption(int optionId);
        OptionDto GetOptionById(int optionId);
        Task AddQuestionAsync(createQuestionDto createquestionDto);
        void savechanges();
    
    }
}
