using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineQuiz.DAL.Data.Models
{
    public class Attempts
    {
        public int Id { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public double Score { get; set; }
        public string StateForExam { get; set; } = "Not Joined";

        // For Quiz
        public int QuizId { get; set; }
        public Quizzes Quiz { get; set; }

        //For Student
        public string  StudentId { get; set; }
        public Student Student { get; set; }


        //For Answers
        public ICollection<Answers> Answers { get; set; } = new HashSet<Answers>();

    }
}
