using OnlineQuiz.BLL.Dtos.Question;
using OnlineQuiz.BLL.Dtos.Track;
using OnlineQuiz.BLL.Managers.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineQuiz.BLL.Managers.Track
{
    public interface ITrackManager 
    {
        IQueryable<TrackDto> GetAll();
        TrackDto GetById(int id);
        void Add(CreateTrackDTO createTrackDTO);
        void Update(TrackDto trackDto);
        void DeleteById(int id);
    }
}
