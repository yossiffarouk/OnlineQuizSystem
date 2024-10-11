using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineQuiz.DAL.Data.Models
{
    public  class Quizzes           
    {
        public int Id { get; set; }
        public string Tittle { get; set; }
        public string Description { get; set; }

        public int QuizDegree { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.Now; // Time when the instructor Create quiz
        public DateTime ExpireDate { get; set; }                   // ExpireDate of Quiz
        public int ExamTime { get; set; }                        // quiz time
        public bool IsAvailable { get; set; } = true;


        public int TracksId { get; set; }
        public Tracks Tracks { get; set; }

        // for Instructor  (foreign Key)
        public string InstructorId { get; set; }
        public Instructor Instructor { get; set; }

        // for Attempt
        public ICollection<Attempts> Attempts { get; set; } = new HashSet<Attempts>();

        //for Question
        public ICollection<Questions> Questions { get; set; } = new HashSet<Questions>();


    }


}

