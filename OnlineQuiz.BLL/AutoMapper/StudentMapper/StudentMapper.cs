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

            CreateMap<Student, StudentReadDto>().ReverseMap();
            CreateMap<Student, StudentAddDto>().ReverseMap();
            CreateMap<Student, StudentUpdateDto>().ReverseMap();
            CreateMap<Student, StudentReadPaginatedDto>().ReverseMap();

            // Map Student to StudentDto
            CreateMap<Quizzes, QuizDetailsForStudentDto>().ReverseMap();
            CreateMap<Instructor, InstructorToStudentDto>().ReverseMap();

            CreateMap<Student, StudentDetailesDto>()
                .ForMember(dest => dest.InstructorToStudentDtos, opt => opt
                .MapFrom(src => src.StudentInstructors.Select(si => si.Instructor)))
                .ForMember(dest => dest.AttemptDetailsDtos, opt => opt
                .MapFrom(src => src.Attempts));

            CreateMap<Attempts, AttemptDetailsDto>()
                .ForMember(dest => dest.QuizDetailsForStudentdto, opt => opt
                .MapFrom(src => src.Quiz));
        }
    }
}


