using OnlineQuiz.BLL.Dtos.Quiz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineQuiz.BLL.Dtos.StudentDtos
{
    public class AttemptDetailsDto
    {
        public int Id { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public double Score { get; set; }
        public string StateForExam { get; set; }
        public QuizDetailsForStudentDto QuizDetailsForStudentdto { get; set; }

    }
}
