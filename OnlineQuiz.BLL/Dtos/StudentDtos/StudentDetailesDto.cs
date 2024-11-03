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
        public string Id { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; } 
        public int Age { get; set; } = 0; 
        public GenderType Gender { get; set; } 
        public UserTypeEnum UserType { get; set; } 
        public string Adress { get; set; } 
        public string ImgUrl { get; set; }
        public string Grade { get; set; } = "Add the Grade";
        public List<AttemptDetailsDto> AttemptDetailsDtos { get; set; } = new List<AttemptDetailsDto>();
        public List<InstructorToStudentDto> InstructorToStudentDtos { get; set; } = new List<InstructorToStudentDto>();

    }
}
