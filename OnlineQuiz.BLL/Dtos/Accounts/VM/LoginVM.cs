using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineQuiz.BLL.Dtos.Accounts.VM
{
    public class LoginVM
    {
      
        public bool successed { get; set; } = false;
        public List<string> Errors { get; set; } = new List<string>();
        public string PropName { get; set; } = string.Empty;
        public string RedirectUrl { get; set; } = "/Home/Login";
    }
}
