using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineQuiz.BLL.Dtos.Attempt
{
    public class QuizReadByIdDto
    {
        public int Id { get; set; }
        public string Title {  get; set; }
        public string Description { get; set; }
    }
}
