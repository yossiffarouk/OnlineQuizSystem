using AutoMapper;
using OnlineQuiz.BLL.Dtos.Admin.InstructorDtos;
using OnlineQuiz.BLL.Dtos.Admin.StudentDtos;
using OnlineQuiz.DAL.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineQuiz.BLL.AutoMapper.AdminAutoMapper
{
    public class AdminMapper :Profile
    {
        public AdminMapper()
        {
           CreateMap<Student, StudentReadDto>().ReverseMap();
           CreateMap<Student, StudentAddDto>().ReverseMap();
           CreateMap<Student, StudentUpdateDto>().ReverseMap();


                                                                                                                            
           CreateMap<Instructor, InstructorReadDto>().ReverseMap();
           CreateMap<Instructor, InstructorAddDto>().ReverseMap();
           CreateMap<Instructor, IstrurctorUpdateDto>().ReverseMap();
           CreateMap<Instructor, InstructorStatusDto>().ReverseMap();
           
        }
    }
}
