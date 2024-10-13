using OnlineQuiz.DAL.Data.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineQuiz.BLL.Dtos.Admin.StudentDtos
{
    public class StudentReadDto
    {

        public string Id { get; set; }
        public string UserName { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public int Age { get; set; }

        public GenderType Gender { get; set; }
        public UserTypeEnum UserType { get; set; }
        public string Adress { get; set; }

        public bool TwoFactorEnabled { get; set; }

        public bool IsBanned { get; set; }
    }
}
