using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace OnlineQuiz.BLL.Dtos.Attempt
{
    public class AttemptDto
    {
        public int Id { get; set; }
        public int QuizId { get; set; }
        public string StudentId { get; set; }
        public double Score { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime? EndTime { get; set; }
        public string stateforexam {  get; set; }

    }
}
