using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineQuiz.BLL.Dtos.Accounts
{
    public class RoleResponce<T>
    {
        public bool successed { get; set; }
        public List<string> Errors { get; set; } = new List<string>();
        public T Data { get; set; }
    }

    public class UserRoleInfo
    {
        public string RoleName { get; set; }
        public int UsersCount { get; set; }
        public List<UserInfo> Users { get; set; }
        public List<GetAllRolesDto> Roles { get; set; }

    }

    public class UserInfo
    {
        public string UserId { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
    }
   

}
