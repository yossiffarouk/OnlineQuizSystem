using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineQuiz.DAL.Data.Models
{
    public class Tracks
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public ICollection<Quizzes> quizzes { get; set; } = new HashSet<Quizzes>(); 
    }
}
