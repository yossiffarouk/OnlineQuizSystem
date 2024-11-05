using AutoMapper;
using OnlineQuiz.BLL.Dtos.Admin.InstructorDtos;
using OnlineQuiz.BLL.Dtos.Admin.StudentDtos;
using OnlineQuiz.BLL.Dtos.Instructor;
using OnlineQuiz.DAL.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineQuiz.BLL.AutoMapper.InstructorMapper
{
    public class InstructorMapper : Profile
    {

        public InstructorMapper()
        {
            CreateMap<Instructor, UpdateInstructorrProfileDto>().ReverseMap();
           

        }

    }
}
