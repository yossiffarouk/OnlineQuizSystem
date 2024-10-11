using OnlineQuiz.DAL.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineQuiz.BLL.Dtos.StudentDtos
{
    public class StudentDetailesDto
    {
        public string UserName { get; set; }
        public string Email { get; set; }
        public int Age { get; set; }
        public GenderType Gender { get; set; }
        public string ImgUrl { get; set; }
        public string Grade { get; set; }
        public List<QuizDetailsForStudentDto> Quizzes { get; set; } = new List<QuizDetailsForStudentDto>();

    }
}
