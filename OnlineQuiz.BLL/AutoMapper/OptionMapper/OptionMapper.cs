using AutoMapper;
using OnlineQuiz.BLL.Dtos.Options;
using OnlineQuiz.BLL.Dtos.Quiz;
using OnlineQuiz.DAL.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineQuiz.BLL.AutoMapper.OptionMapper
{
    public class OptionMapper:Profile
    {
        public OptionMapper()
        {
            CreateMap<Option, OptionDto>().ReverseMap();
            CreateMap<Option, createOptionDto>().ReverseMap();
        }
    }
}
