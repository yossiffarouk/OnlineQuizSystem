using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineQuiz.BLL.Dtos.Admin.Roles
{
    public class AddRoleToUser
    {
        public string UserId { get; set; }
        public string RoleId { get; set; }
    }
}
