using AutoMapper;
using OnlineQuiz.BLL.Dtos.Options;
using OnlineQuiz.BLL.Dtos.Question;
using OnlineQuiz.BLL.Dtos.Quiz;
using OnlineQuiz.DAL.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineQuiz.BLL.AutoMapper.QuestionMapper
{
    public class QuestionMapper:Profile
    {
        public QuestionMapper()
        {
            CreateMap<Questions, QuestionDto>().ReverseMap();
            CreateMap<Questions, createQuestionDto>().ReverseMap();
        }
    }
}
