using AutoMapper;
using OnlineQuiz.BLL.Dtos.Quiz;
using OnlineQuiz.BLL.Dtos.Track;
using OnlineQuiz.DAL.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineQuiz.BLL.AutoMapper.QuizMapper
{
    public class QuizMapper:Profile
    {
        public QuizMapper()
        {
            CreateMap<Quizzes, QuizDto>().ReverseMap();
            CreateMap<Quizzes,CreatQuizDTO>().ReverseMap();
            CreateMap<Quizzes, FinalQuizDTO>().ReverseMap();

        }
    }
}
