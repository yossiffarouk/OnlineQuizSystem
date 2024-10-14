using AutoMapper;
using OnlineQuiz.BLL.Dtos.Track;
using OnlineQuiz.DAL.Data.Models;
using OnlineQuiz.DAL.Repositoryies.Base;
using OnlineQuiz.DAL.Repositoryies.TrackRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineQuiz.BLL.Managers.Track
{
    public class TrackManager : ITrackManager
    {
        private readonly ITrackRepository _trackRepository;
        private readonly IMapper _mapper;

        public TrackManager(ITrackRepository trackRepository, IMapper mapper)
        {
            _trackRepository = trackRepository;
            _mapper = mapper;
        }
        public IQueryable<TrackDto> GetAll()
        {
            var tracks = _trackRepository.GetAll();
            return _mapper.ProjectTo<TrackDto>(tracks);
        }
        public TrackDto GetById(int id)
        {
            var track = _trackRepository.GetById(id);
            return _mapper.Map<TrackDto>(track);
        }
        public void Add(CreateTrackDTO trackDto)
        {
            var track = _mapper.Map<Tracks>(trackDto);
            _trackRepository.Add(track);
        }
        public void Update(TrackDto trackDto)
        {
            var track = _mapper.Map<Tracks>(trackDto);
            if (track == null || track.IsDeleted) // Check if track exists and is not deleted
            {
                throw new Exception("Track not found or has been deleted.");
            }
            _trackRepository.Update(track);
        }
        public void DeleteById(int id)
        {
            var existingTrack = _trackRepository.GetById(id);
            if (existingTrack == null || existingTrack.IsDeleted)
            {
                throw new Exception("Track not found or already deleted."); 
            }
            _trackRepository.DeleteById(id);
        }

    
    }
}
