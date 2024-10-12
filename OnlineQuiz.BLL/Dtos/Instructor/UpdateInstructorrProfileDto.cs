using OnlineQuiz.DAL.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineQuiz.BLL.Dtos.Instructor
{
    public class UpdateInstructorrProfileDto
    {
        public string UserName { get; set; }
        public int Age { get; set; }
        public string Address { get; set; }
        public string ImgUrl { get; set; }

        public string PhoneNumber { get; set; }

        public GenderType Gender { get; set; }

    }
}
