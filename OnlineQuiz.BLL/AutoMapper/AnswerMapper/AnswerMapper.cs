using AutoMapper;
using OnlineQuiz.BLL.Dtos.Answer;
using OnlineQuiz.BLL.Dtos.Attempt;
using OnlineQuiz.BLL.Dtos.Track;
using OnlineQuiz.DAL.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineQuiz.BLL.AutoMapper.AnswerMapper
{
    public class AnswerMapper: Profile
    {
        public AnswerMapper()
        {

            CreateMap<Answers, AnswerDto>().ReverseMap();

        }
    }
}
