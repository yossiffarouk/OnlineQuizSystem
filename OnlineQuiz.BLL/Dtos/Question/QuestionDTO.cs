using OnlineQuiz.BLL.Dtos.Options;
using OnlineQuiz.DAL.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineQuiz.BLL.Dtos.Question
{
    public class QuestionDto
    {
        public int Id { get; set; }
        public string Tittle { get; set; }
        public string CorrectAnswer { get; set; }
        // For Quiz
        public int QuizId { get; set; }
       
        // Include a list of option DTOs if needed
          public ICollection<OptionDto> Options { get; set; } = new HashSet<OptionDto>();
    }
}
