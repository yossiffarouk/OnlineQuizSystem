using OnlineQuiz.BLL.Dtos.Attempts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OnlineQuiz.BLL.Dtos.Attempt;
using OnlineQuiz.BLL.Dtos.Quiz;

namespace OnlineQuiz.BLL.Dtos.Answer
{
    public class ResultDto
    {
        public GetResultAttemptDto Score { get; set; }
        public FinalQuizDTO quiz { get; set; }
        public ICollection<AnswerDto> Answers { get; set; } = new HashSet<AnswerDto>();

    }
}
