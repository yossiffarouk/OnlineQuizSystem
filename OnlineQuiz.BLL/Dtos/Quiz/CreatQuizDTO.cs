using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineQuiz.BLL.Dtos.Quiz
{
    public class CreatQuizDTO
    {
        [Required(ErrorMessage = "Title is required.")]
        [StringLength(100, ErrorMessage = "Title cannot exceed 100 characters.")]
        public string Tittle { get; set; }

        [Required(ErrorMessage = "Description is required.")]
        public string Description { get; set; }

        [Range(1, 60, ErrorMessage = "Quiz degree must be between 1 and 60.")]
        public int QuizDegree { get; set; }

        public DateTime CreatedDate { get; set; }

        [Required(ErrorMessage = "Expire date is required.")]
        public DateTime ExpireDate { get; set; }

        public bool IsAvailable { get; set; } = true;

        [Range(1, 120, ErrorMessage = "Exam time must be between 1 and 120 minutes.")]
        public int ExamTime { get; set; }

        [Required(ErrorMessage = "Track ID is required.")]
        public int TracksId { get; set; }

        [Required(ErrorMessage = "Instructor ID is required.")]
        public string InstructorId { get; set; }
    }
}
