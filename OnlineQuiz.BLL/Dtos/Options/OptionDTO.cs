using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineQuiz.BLL.Dtos.Options
{
    public class OptionDto
    {
        public int OptionId { get; set; }
        public string OptionText { get; set; }

        // Foreign Key reference to Question
      //  public int QuestionId { get; set; }
    }
}
