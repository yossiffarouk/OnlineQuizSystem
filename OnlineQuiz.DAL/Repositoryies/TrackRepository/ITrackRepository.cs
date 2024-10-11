using OnlineQuiz.DAL.Data.Models;
using OnlineQuiz.DAL.Repositoryies.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineQuiz.DAL.Repositoryies.TrackRepository
{
    public interface ITrackRepository  : IRepository<Tracks,int>
    {
        //any specific methods for Track here if needed
    }
}
