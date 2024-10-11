using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineQuiz.BLL.Dtos.Attempt
{
    public class SubmitQuizDto
    {
        public int AttemptId { get; set; }
        
        public List<AnswerDto> SubmittedAnswers { get; set; }

    }
}
