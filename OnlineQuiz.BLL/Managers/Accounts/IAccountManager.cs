using Microsoft.AspNetCore.Mvc;
using OnlineQuiz.BLL.Dtos.Accounts;
using OnlineQuiz.DAL.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineQuiz.BLL.Managers.Accounts
{
    public interface IAccountManager
    {
        Task<LoginResponce> Login(LoginDto loginDto);
        Task<GeneralRespnose> Register(RegisterDto registerDto , IUrlHelper urlHelper);
        Task<GeneralRespnose> ConfirmEmail(string userId, string token);
        Task<GeneralRespnose> ForgotPassword(ForgotPasswordDto forgotPasswordDto, IUrlHelper urlHelper);
        Task<GeneralRespnose> ResetPassword(ResetPasswordDto resetPasswordDto);
        Task<GeneralRespnose> AddRole(string RoleName);
        Task<GeneralRespnose> DeleteRole(string  RoleId);
        Task<GeneralRespnose> AddRoleToUser(string UserId,  string RoleId);
        Task<GeneralRespnose> RemoveRoleFromUser(string UserId, string RoleId);
        Task<RoleResponce<IQueryable<string>>> GetAllRoles();
        Task<RoleResponce<IQueryable<string>>> GetAllRolesIsDeleted();
        Task<RoleResponce<UserRoleInfo>> GetUsersInRole(string RoleId);



    }
}
