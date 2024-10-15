using OnlineQuiz.DAL.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineQuiz.BLL.Dtos.Accounts
{
    public class RoleMapping
    {
        public static Dictionary<string, UserTypeEnum> RoleToUserTypeMap = new Dictionary<string, UserTypeEnum>();

        public void AddRoleMappingToUserType(string roleName, UserTypeEnum userType)
        {
            if (!RoleToUserTypeMap.ContainsKey(roleName))
            {
                // Adds new mapping
                RoleToUserTypeMap.Add(roleName, userType);
            }
        }

        public void RemoveRoleMappingFromUserType(string roleName)
        {
            if (RoleToUserTypeMap.ContainsKey(roleName))
            {
                // Removes the role mapping
                RoleToUserTypeMap.Remove(roleName); 
            }
        }


    }

}
