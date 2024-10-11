using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineQuiz.DAL.Data.Models
{
    public class Option
    {
        public int OptionId { get; set; }
        public string OptionText { get; set; }

        // Foreign Key

        // for Question
       public int QuestionsId { get; set; }
      public Questions Questions { get; set; }
    }
}
