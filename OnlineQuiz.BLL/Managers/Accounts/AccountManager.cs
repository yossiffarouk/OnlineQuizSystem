using Azure;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using OnlineQuiz.BLL.Dtos.Accounts;
using OnlineQuiz.DAL.Data.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace OnlineQuiz.BLL.Managers.Accounts
{
    public class AccountManager : IAccountManager
    {
        private readonly UserManager<Users> _userManager;
        private readonly IConfiguration _configuration;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IEmailService _emailService;

        public AccountManager(UserManager<Users> userManager, IConfiguration configuration, RoleManager<IdentityRole> roleManager
            ,IEmailService emailService )
        {
            _userManager = userManager;
            _configuration = configuration;
            _roleManager = roleManager;
             _emailService = emailService;
        }


     public async Task<GeneralRespnose> Register(RegisterDto registerDto, IUrlHelper urlHelper)
        {
            var response = new GeneralRespnose();
            if (_userManager.Users.Any(s => s.UserName == registerDto.UserName))
            {
                response.Errors.Add("Username is already taken. Please choose another one.");
                return response;

            }
            if (_userManager.Users.Any(s => s.Email == registerDto.Email))
            {
                response.Errors.Add("Email Is Already Exist");
                return response;

            }

            //  if the user is registering as an Admin
            if (registerDto.UserType == UserTypeEnum.Admin)
            {
                response.Errors.Add("Super admin has not agreed to create an admin account.");
                return response;
            }

            if (registerDto.Password != registerDto.ConfirmedPassword)
            {
                response.Errors.Add("Password and confirmation password do not match.");
                return response;
            }


            // Determine user type
            Users user = registerDto.UserType == UserTypeEnum.Student ? new OnlineQuiz.DAL.Data.Models.Student() :
                    (registerDto.UserType == UserTypeEnum.Instructor ? new OnlineQuiz.DAL.Data.Models.Instructor() : new  OnlineQuiz.DAL.Data.Models.Student());


            user.UserName = registerDto.UserName;
            user.Email = registerDto.Email;
            user.UserType = registerDto.UserType;
            user.Gender= registerDto.Gender;
            user.Adress = registerDto.Address;
            user.Age = registerDto.Age;
            
           

            var result = await _userManager.CreateAsync(user, registerDto.Password);
            if (result.Succeeded)
            {

                #region VerifyEmail
                // Generate the email confirmation token
                var emailConfirmationToken = await _userManager.GenerateEmailConfirmationTokenAsync(user);

                //  Generate the email confirmation link using UrlHelper
                var confirmationLink = urlHelper.Action("ConfirmEmail", "Accounts",
                    new { userId = user.Id, token = emailConfirmationToken }, "https");

                // Send  confirmation link  
                var confirmationEmailBody = $"Dear {user.UserName},\n\n" +
                               "Thank you for registering with us!\n\n" +
                               "To complete your registration, please confirm your email address by clicking the link below:\n" +
                               $"{confirmationLink}\n\n" +
                               "If you did not create an account, please ignore this email.\n\n" +
                               "Best regards,\n" +
                               "[Online Quiz Platform]\n" +
                               "[+20 155 134 9812]";

              var res =  await _emailService.SendEmailAsync(user.Email, "Confirm Your Email Address", confirmationEmailBody);
                if (!res.successed)
                {
                    response.Errors.AddRange(res.Errors);
                    return response;
                }

                

                #endregion

                // Assign the role 
                await _userManager.AddToRoleAsync(user, registerDto.UserType == UserTypeEnum.Student ? Roles.Student :
                          (registerDto.UserType == UserTypeEnum.Instructor ? Roles.Instructor : Roles.Admin));



                response.successed = true;
                return response;
            }

            response.Errors = result.Errors.Select(d => d.Description).ToList();
            return response;
        }

     public async Task<LoginResponce> Login(LoginDto loginDto)
        {
            var response = new LoginResponce();
            var user = await _userManager.FindByEmailAsync(loginDto.Email);
            if (user == null || !await _userManager.IsEmailConfirmedAsync(user))
            {
                response.Errors.Add(user == null ? "Email not found. Please make sure the email is correct." :
                   "Email not confirmed. Please check your inbox.");
                return response;
            }
       

            var result = await _userManager.CheckPasswordAsync(user, loginDto.Password);
            if (result)
            {
                #region Claims
                List<Claim> claims = new List<Claim>()
                {
                new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()), // User ID as Subject
                new Claim(JwtRegisteredClaimNames.Email, user.Email), // User email
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())// Token identifie
                };
                var UserRoles = await _userManager.GetRolesAsync(user);
                foreach (var role in UserRoles)
                {
                    claims.Add(new Claim(ClaimTypes.Role, role));
                }
                #endregion
                await _userManager.AddClaimsAsync(user, claims);
                response.Token = GenerateToken(claims , loginDto.RememberMe);
                response.successed = true;
                return response;

            }
            response.Errors.Add("Wrong Password or Email");
            return response;
        }

     public async Task <GeneralRespnose> ConfirmEmail(string userId, string token)
        {
            GeneralRespnose response = new GeneralRespnose();   
            if (string.IsNullOrWhiteSpace(userId) || string.IsNullOrWhiteSpace(token))
            {
                response.Errors.Add("UserId and Token are required.");
                return response;
            }

            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                response.Errors.Add("User not found.");
                return response; 
            }

            var result = await _userManager.ConfirmEmailAsync(user, token);
            if (result.Succeeded)
            {
                response.successed = true;
                return response;
              
            }
            response.Errors.Add("Email confirmation failed."); ;
            return response;
       
        }

     public async Task<GeneralRespnose> ForgotPassword( ForgotPasswordDto forgotPasswordDto , IUrlHelper urlHelper)
        {
            var response = new GeneralRespnose();   
            var user = await _userManager.FindByEmailAsync(forgotPasswordDto.Email);
            if(user == null)
            {
                response.Errors.Add("Email not found. Please make sure the email is correct.");
                return (response);
            }

            #region ResetPaswword

            //  reset password token
            var token = await _userManager.GeneratePasswordResetTokenAsync(user);

            //  password reset link
            var resetLink = urlHelper.Action("ResetPassword", "Accounts",
                new { token, email = user.Email }, "https");

            // Send email
            var resetEmailBody = $"Dear {user.UserName},\n\n" +
                         "We received a request to reset your password.\n\n" +
                         $"Please click the link below to set a new password:\n" +
                         $"{resetLink}\n\n" +
                         "If you did not request a password reset, please ignore this email or contact support if you have concerns.\n\n" +
                         "Best regards,\n" +
                          "[Online Quiz Platform]\n" +
                          "[+20 155 134 9812]";

            var result = await _emailService.SendEmailAsync(user.Email, "Reset Your Password", resetEmailBody); 
           

            if (result.successed)
            {
                response.successed = result.successed;
                return response;
            }

            response.Errors.AddRange(result.Errors);
            return response;
            #endregion
        }

     public async Task<GeneralRespnose> ResetPassword(ResetPasswordDto resetPasswordDto)
        {
            var response = new GeneralRespnose();
            if(resetPasswordDto.NewPassword != resetPasswordDto.ConfirmedNewPassword)
            {
                response.Errors.Add("New password and confirmation password do not match.");
                return response;
            }
            var user = await _userManager.FindByEmailAsync(resetPasswordDto.Email);
            if (user == null)
            {
                response.Errors.Add("Email not found. Please make sure the email is correct.");
                return response;
            }
            var decodedToken = HttpUtility.UrlDecode(resetPasswordDto.Token);
            var resetpaswword = await _userManager.ResetPasswordAsync(user, decodedToken, resetPasswordDto.NewPassword);
            if(resetpaswword.Succeeded)
            {
                response.successed= resetpaswword.Succeeded;
                return response;
            }
            response.Errors =resetpaswword.Errors.Select(e=>e.Description).ToList();
            return response;
        }

        public async Task<GeneralRespnose> AddRole(string RoleName)
        {
            var response = new GeneralRespnose();

            var roleExists = await _roleManager.RoleExistsAsync(RoleName);
            if (roleExists)
            {
                response.Errors.Add("Role already exists.");
                return response;
            }

            var roleResult = await _roleManager.CreateAsync(new IdentityRole(RoleName));
            if (roleResult.Succeeded)
            {
                response.successed = true;
                return response;
            }

            response.Errors.AddRange(roleResult.Errors.Select(e => e.Description));
            return response;
        }

        public async Task<GeneralRespnose> DeleteRole(string RoleName)
        {
            var response = new GeneralRespnose();

            var role = await _roleManager.FindByNameAsync(RoleName);
            if (role == null)
            {
                response.Errors.Add("Role not found.");
                return response;
            }


            var roleResult = await _roleManager.DeleteAsync(role);
            if (roleResult.Succeeded)
            {
                response.successed = true;
                return response;
            }
            response.Errors.AddRange(roleResult.Errors.Select(e => e.Description));
            return response;
        }
        
        public async Task<GeneralRespnose> AddRoleToUser(string UserId, string RoleName)
        {
            var response = new GeneralRespnose();

            var user = await _userManager.FindByIdAsync(UserId);
            if (user == null)
            {
                response.Errors.Add("User not found.");
                return response;
            }
     
            var roleExists = await _roleManager.RoleExistsAsync(RoleName);
            if (!roleExists)
            {
                response.Errors.Add("Role does not exist.");
                return response;
            }

            var result = await _userManager.AddToRoleAsync(user, RoleName);
            if (result.Succeeded)
            {
                response.successed = true;
                return response;             
            }
            response.Errors.AddRange(result.Errors.Select(e => e.Description));
            return response;
        }

        public async Task<GeneralRespnose> RemoveRoleFromUser(string UserId, string RoleName)
        {
            var response = new GeneralRespnose();

            var user = await _userManager.FindByIdAsync(UserId);
            if (user == null)
            {
                response.Errors.Add("User not found.");
                return response;
            }

          
            var isInRole = await _userManager.IsInRoleAsync(user, RoleName);
            if (!isInRole)
            {
                response.Errors.Add("User is not in this role.");
                return response;
            }

           
            var result = await _userManager.RemoveFromRoleAsync(user, RoleName);
            if (result.Succeeded)
            {
                response.successed = true;
                return response;           
            }
            response.Errors.AddRange(result.Errors.Select(e => e.Description));
            return response;
        }

        public async Task<RoleResponce<IEnumerable<string>>> GetAllRoles()
        {
            var response = new RoleResponce<IEnumerable<string>>();
            var roles = _roleManager.Roles.ToList();
            response.Data = roles.Select(s => s.Name).ToList(); 

            response.successed = true;
            return response;
        }

        public async Task<RoleResponce<UserRoleInfo>> GetUsersInRole(string RoleName)
        {
            var response = new RoleResponce<UserRoleInfo>();

    
            var roleExists = await _roleManager.RoleExistsAsync(RoleName);
            if (!roleExists)
            {
                response.Errors.Add("Role does not exist.");
                return response;
            }

            // Get users in the specified role
            var users = await _userManager.GetUsersInRoleAsync(RoleName);
            var userCount = users.Count();

            // Prepare the response data
            response.Data = new UserRoleInfo
            {
                RoleName = RoleName,
                UsersCount = userCount,
                Users = users.Select(u => new UserInfo
                {
                    UserId = u.Id,
                    UserName = u.UserName,
                    Email = u.Email
                }).ToList()
              
            };

            response.successed = true;
            return response;
        }






        private string GenerateToken(IList<Claim> claims , bool RememberMe)
        {

            #region Token
            #region SecurityKey
            var SecretKeyString = _configuration.GetSection("SecretKey").Value;
            var SecretKeyByte = Encoding.ASCII.GetBytes(SecretKeyString);
            SecurityKey securityKey = new SymmetricSecurityKey(SecretKeyByte);
            #endregion

            //Combind SecretKey , HasingAlgorithm (SigningCredentials)
            SigningCredentials signingCredential = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            //Determine Expiration
            DateTime tokenExpiration = RememberMe ? DateTime.Now.AddDays(30) : DateTime.Now.AddHours(2);

            //Token

            JwtSecurityToken jwtSecurityToken = new JwtSecurityToken
            (
                claims: claims,
                signingCredentials: signingCredential,
                expires: tokenExpiration
            );
            //To convert Token To String
            JwtSecurityTokenHandler handler = new JwtSecurityTokenHandler();
            string token = handler.WriteToken(jwtSecurityToken);

            #endregion

            return token;
        }

   
    }
    }




