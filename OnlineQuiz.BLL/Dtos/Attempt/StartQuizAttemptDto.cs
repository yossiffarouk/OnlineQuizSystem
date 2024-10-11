using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineQuiz.BLL.Dtos.Attempt
{
    public class StartQuizAttemptDto
    {
        public int QuizId { get; set; }
        public string StudentId { get; set; }
    }
}
