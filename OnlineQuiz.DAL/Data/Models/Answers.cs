using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineQuiz.DAL.Data.Models
{
    public class Answers
    {
        public int Id { get; set; } 
       public string SubmittedAnswer { get; set; }
        public bool IsCorrect { get; set; } 

        // for Attempt
        public int AttemptId { get; set; }
        public virtual Attempts Attempt { get; set; }


        // for Question
        public int  QuestionId { get; set; }    
        public virtual Questions Question { get; set; }
    }
}
