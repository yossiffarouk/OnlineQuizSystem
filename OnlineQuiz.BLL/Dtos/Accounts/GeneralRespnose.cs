using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineQuiz.BLL.Dtos.Accounts
{
    public class GeneralRespnose
    {
        public bool successed { get; set; } = false;
        public List<string> Errors { get; set; }
    }
}
