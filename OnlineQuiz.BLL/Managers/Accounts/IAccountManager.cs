using Microsoft.AspNetCore.Mvc;
using OnlineQuiz.BLL.Dtos.Accounts;
using OnlineQuiz.BLL.Dtos.Accounts.VM;
using OnlineQuiz.DAL.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace OnlineQuiz.BLL.Managers.Accounts
{
    public interface IAccountManager
    {
        Task<GeneralRespnose> Register(RegisterDto registerDto, IUrlHelper urlHelper);
        Task<LoginResponce> Login(LoginDto loginDto);
        Task<LoginVM> LoginMVC(LoginDto loginDto);
        Task Logout();

        Task<GeneralRespnose> ConfirmEmail(string userId, string token);
        Task<GeneralRespnose> ResendConfirmationEmail(string userId, IUrlHelper Url);
        Task<GeneralRespnose> ForgotPassword(ForgotPasswordDto forgotPasswordDto, IUrlHelper urlHelper);
        Task<GeneralRespnose> ResetPassword(ResetPasswordDto resetPasswordDto);

        Task<GeneralRespnose> AddRole(string RoleName);
        Task<GeneralRespnose> DeleteRole(string  RoleId);
        Task<GeneralRespnose> RestoreRole(string RoleId);
        Task<GeneralRespnose> AddRoleToUser(string UserId,  string RoleId);
        Task<GeneralRespnose> RemoveRoleFromUser(string UserId, string RoleId);
        Task<List<GetAllRolesDto>>GetAllRoles();
        Task<List<GetAllRolesDto>> GetAllRolesIsDeleted();
        Task<RoleResponce<UserRoleInfo>> GetUsersInRole(string RoleId);


        Task<List<UserTypeEnum>> GetAllUserTypes();
        Task<List<GenderType>> GetAllGenderTypes();

        Task<string>GetUserIdByGmail(string Gmail);
        
    }
}
