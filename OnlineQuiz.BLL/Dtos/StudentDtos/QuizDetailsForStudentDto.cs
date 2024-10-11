using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineQuiz.BLL.Dtos.StudentDtos
{
    public class QuizDetailsForStudentDto
    {
        public int QuizId { get; set; }
        public string QuizTitle { get; set; }
        public double Score { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
    }
}
