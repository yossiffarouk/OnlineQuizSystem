using AutoMapper;
using OnlineQuiz.BLL.Dtos.Track;
using OnlineQuiz.DAL.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineQuiz.BLL.AutoMapper.TrackMapper
{
    public class TrackMapper:Profile
    {
        public TrackMapper()
        {
           
                CreateMap<Tracks, TrackDto>().ReverseMap();
            CreateMap<Tracks, CreateTrackDTO>().ReverseMap();

        }
    }
}
