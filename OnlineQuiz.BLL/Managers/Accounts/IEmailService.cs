using OnlineQuiz.BLL.Dtos.Accounts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineQuiz.BLL.Managers.Accounts
{
    public interface  IEmailService
    {
        Task<GeneralRespnose> SendEmailAsync(string email, string subject, string message);
    }
}
