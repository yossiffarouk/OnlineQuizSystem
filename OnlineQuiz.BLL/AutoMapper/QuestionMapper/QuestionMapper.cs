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
            // Map from createQuestionDto to Questions entity
            CreateMap<createQuestionDto, Questions>()
                .ForMember(dest => dest.Options, opt => opt.Ignore()); // Ignore Options if they're managed separately

            // Map from Questions entity to QuestionDto (if needed)
            CreateMap<Questions, QuestionDto>()
                .ForMember(dest => dest.Options, opt => opt.MapFrom(src => src.Options)); // Assuming Options is a list in your DTO
        }
    }
}
