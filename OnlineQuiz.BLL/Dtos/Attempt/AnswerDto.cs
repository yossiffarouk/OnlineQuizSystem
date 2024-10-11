using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineQuiz.BLL.Dtos.Attempt
{
    public class AnswerDto
    {
        public int queid { get; set; }
        public string SubmittedAnswer { get; set; }
        public bool IsCorrect { get; set; } 
    }
}
