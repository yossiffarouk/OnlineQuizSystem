using OnlineQuiz.BLL.Dtos.Question;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineQuiz.BLL.Dtos.Quiz
{
    public class FinalQuizDTO
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int QuizDegree { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime ExpireDate { get; set; }
        public int ExamTime { get; set; }
        public bool IsAvailable { get; set; }

        
        public ICollection<QuestionDto> Questions { get; set; } = new List<QuestionDto>();
    }
}
