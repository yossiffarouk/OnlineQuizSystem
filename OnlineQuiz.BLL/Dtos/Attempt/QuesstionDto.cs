using OnlineQuiz.BLL.Dtos.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineQuiz.BLL.Dtos.Attempts
{
    public class QuesstionDto
    {
        public int Id { get; set; }
        public string Tittle { get; set; }
        public int attemptid { get; set; }
        public List<OptionsDto> options { get; set; }
    }
}
