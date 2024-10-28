using OnlineQuiz.BLL.Dtos.Options;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineQuiz.BLL.Dtos.Question
{
    public class CreateQuestionVM
    {
        public string Tittle { get; set; }
        [Required(ErrorMessage = "Please select the correct answer.")]
        public string CorrectAnswer { get; set; }
        // For Quiz
        //public int QuizId { get; set; }

        // Include a list of option DTOs if needed
        public List<createOptionDto> Options { get; set; } = new List<createOptionDto>();

    }
}
