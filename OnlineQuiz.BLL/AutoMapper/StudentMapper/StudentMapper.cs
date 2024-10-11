using AutoMapper;
using OnlineQuiz.BLL.Dtos.StudentDtos;
using OnlineQuiz.DAL.Data.Models;
using OnlineQuiz.BLL.AutoMapper;
using OnlineQuiz.BLL.Dtos.StudentDtos;
using OnlineQuiz.DAL.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineQuiz.BLL.AutoMapper.StudentMapper
{
    public class StudentMapper : Profile
    {
        public StudentMapper()
        {
            CreateMap<Student, StudentReadDto>();

            CreateMap<Student, StudentReadDto>().ReverseMap();
            CreateMap<Student, StudentAddDto>().ReverseMap();
            CreateMap<Student, StudentUpdateDto>().ReverseMap();
            // Map Student to StudentDto
            CreateMap<Student, StudentDetailesDto>()
                    .ForMember(dest => dest.Quizzes, opt => opt
                    .MapFrom(src => src.Attempts));
            // Map Attempts to QuizDetailsDto
            CreateMap<Attempts, QuizDetailsForStudentDto>()
           .ForMember(dest => dest.QuizId, opt => opt.MapFrom(src => src.QuizId))
           .ForMember(dest => dest.QuizTitle, opt => opt.MapFrom(src => src.Quiz.Tittle))
           .ForMember(dest => dest.Score, opt => opt.MapFrom(src => src.Score))
           .ForMember(dest => dest.StartTime, opt => opt.MapFrom(src => src.StartTime))
           .ForMember(dest => dest.EndTime, opt => opt.MapFrom(src => src.EndTime));
        }
    }
}
