using Microsoft.EntityFrameworkCore;
using OnlineQuiz.DAL.Data.DBHelper;
using OnlineQuiz.DAL.Data.Models;
using OnlineQuiz.DAL.Repositoryies.Base;
using System;
using System.Collections.Generic;

using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineQuiz.DAL.Repositoryies.TrackRepository
{
    public class TrackRepository : IRepository<Tracks, int>, ITrackRepository
    {
        private readonly QuizContext _context;

        public TrackRepository(QuizContext context)
        {
            _context = context;
        }   
        public IQueryable<Tracks> GetAll()
        {
            return _context.tracks.Where(t => !t.IsDeleted).AsQueryable();
        }
        public Tracks GetById(int id)
        {
            return _context.tracks.FirstOrDefault(q => q.Id == id && !q.IsDeleted);
        }
        public void Add(Tracks track)
        {
            _context.tracks.Add(track);
            _context.SaveChanges();
        }
        public void Update(Tracks track)
        {
            if (_context.Entry(track).State == EntityState.Detached)
            {
                _context.tracks.Attach(track);
            }
            _context.Entry(track).State = EntityState.Modified;
            _context.SaveChanges();
        }
        public void DeleteById(int id)
        {
            var track = _context.tracks.Find(id);
            if (track != null)
            {
                track.IsDeleted = true; // Set the IsDeleted flag
                _context.SaveChanges();
            }
        }
    }
}
