using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineQuiz.BLL.Dtos.Attempt
{
    public class AnswerDto
    {
        public int QuestionId { get; set; }
        public int AttemptId { get; set; }
        public string SubmittedAnswer { get; set; }
    }
}
