using OnlineQuiz.DAL.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineQuiz.BLL.Dtos.StudentDtos
{
    public class StudentAddDto
    {
        public string Name { get; set; }
        public String Email { get; set; }
        public string PhoneNumber { get; set; }
        public string UserName { get; set; }
        public int Age { get; set; }
        public string Grade { get; set; }
        public string ImgUrl { get; set; }
        public GenderType Gender { get; set; }
        public string Adress { get; set; }
    }
}
