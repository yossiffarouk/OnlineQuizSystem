using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineQuiz.BLL.Dtos.Attempt
{
    public class QuizScoreDto
    {
        public int QuizId { get; set; }
        public string QuizTitle { get; set; }
        public double TotalScore { get; set; }
        public string Grade { get; set; }
    }
}
