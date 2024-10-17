using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineQuiz.BLL.Dtos.Attempt
{
    public class GetUserAttemptsDto
    {
        
        public int AttemptID { get; set;}
        public string StudentName { get; set; }
        public string QuizName { get; set;}
        public int Score {  get; set;}
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
    }
}
