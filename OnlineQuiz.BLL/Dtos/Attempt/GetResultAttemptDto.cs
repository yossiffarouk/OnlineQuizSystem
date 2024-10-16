using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineQuiz.BLL.Dtos.Attempt
{
    public class GetResultAttemptDto
    {
        public string QuizName {  get; set; }
        public string StudentName { get; set; }
        public int TotalAnswers {  get; set; }
        public int CorrectAnswers {  get; set; }
        public int WrongAnswers {  get; set; }
        public int Score { get; set; }
    }
}
