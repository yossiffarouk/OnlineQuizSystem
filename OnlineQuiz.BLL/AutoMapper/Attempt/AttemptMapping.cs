using AutoMapper;
using OnlineQuiz.BLL.Dtos.Attempt;
using OnlineQuiz.BLL.Dtos.Instructor;
using OnlineQuiz.DAL.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineQuiz.BLL.AutoMapper.Attempt
{
    public class AttemptMapping : Profile
    {
       
        public AttemptMapping() 
        {
            CreateMap<Attempts, AttemptDto>();
            CreateMap<AnswerDto, Answers>().ReverseMap();
            CreateMap<Quizzes, QuizReadByIdDto>();
            CreateMap<Student, StudentReadByIdDto>();
            CreateMap<Attempts, StudentAttemptDto>().ReverseMap();
        }

    }
}
