using Azure;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.AspNetCore.Rewrite;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using OnlineQuiz.BLL.Dtos.Accounts;
using OnlineQuiz.BLL.Dtos.Accounts.VM;
using OnlineQuiz.DAL.Data.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net;
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
        private readonly RoleManager<CustomRole> _roleManager;
        private readonly IEmailService _emailService;
        private readonly SignInManager<Users> _signInManager;

        public AccountManager(UserManager<Users> userManager, IConfiguration configuration, RoleManager<CustomRole> roleManager
            ,IEmailService emailService ,SignInManager<Users> signInManager )
        {
            _userManager = userManager;
            _configuration = configuration;
            _roleManager = roleManager;
             _emailService = emailService;
            _signInManager = signInManager;
        }

      
     public async Task<GeneralRespnose> Register(RegisterDto registerDto, IUrlHelper urlHelper)
        {
            var response = new GeneralRespnose();

            if (_userManager.Users.Any(s => s.UserName == registerDto.UserName))
            {
                response.Errors.Add("Username is already taken. Please choose another .");
                response.PropertyName = nameof(registerDto.UserName);
                return response;

            }
            if (_userManager.Users.Any(s => s.Email == registerDto.Email))
            {
                response.Errors.Add("Email  Already Exist");
                response.PropertyName = nameof(registerDto.Email);
                return response;


            }

            //  if the user is registering as an Admin
            if (registerDto.UserType == UserTypeEnum.Admin )
            {
                response.Errors.Add("Super admin has not agreed to create an admin account.");
                response.PropertyName = nameof(registerDto.UserType);
                return response;

            }

            if (registerDto.Password != registerDto.ConfirmedPassword)
            {
                response.Errors.Add("Password and confirmation password do not match.");
                response.PropertyName = nameof(registerDto.ConfirmedPassword);
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
               await _signInManager.SignInAsync(user, isPersistent: false);
                #region VerifyEmail
                // Generate the email confirmation token
                var emailConfirmationToken = await _userManager.GenerateEmailConfirmationTokenAsync(user);

                //  Generate the email confirmation link using UrlHelper
                var confirmationLink = urlHelper.Action("ConfirmEmail", "Home",
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
                response.PropName = nameof(loginDto.Email);
                return response;
            }
           
            if (user.IsBanned)
            {
                response.Errors.Add("Your account has been banned.");
                return response;
            }
            if (user.UserType == UserTypeEnum.Instructor)
            {
                var instructor = user as OnlineQuiz.DAL.Data.Models.Instructor; 

                if (instructor != null)
                {
                    if (instructor.Status == ApprovalStatus.Pending)
                    {
                        response.Errors.Add("Your account is pending approval by the admin.");
                        return response;
                    }
                    else if (instructor.Status == ApprovalStatus.Denied)
                    {
                        response.Errors.Add("Your account has been denied by the admin.");
                        return response;
                    }
                }
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
            response.PropName=nameof(loginDto.Password);
            return response;
        }

     public async Task< LoginVM> LoginMVC(LoginDto loginDto)
        {
            var response = new LoginVM();
            var user = await _userManager.FindByEmailAsync(loginDto.Email);
            if (user == null || !await _userManager.IsEmailConfirmedAsync(user))
            {
                response.Errors.Add(user == null ? "Email not found. Please make sure the email is correct." :
                   "Email not confirmed. Please check your inbox.");
                response.PropName = nameof(loginDto.Email);
                return response;
            }

            if (user.IsBanned)
            {
                response.Errors.Add("Your account has been banned.");
                return response;
            }
          
          

            var result = await _signInManager.PasswordSignInAsync(user, loginDto.Password , loginDto.RememberMe,false);
            if (result.Succeeded)
            {
                #region Claims
                List<Claim> claims = new List<Claim>()
                {
                     new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()), // User ID
                    new Claim(ClaimTypes.Email, user.Email), // User email
                    new Claim(ClaimTypes.Name, user.UserName), // Username

   
                };
                var UserRoles = await _userManager.GetRolesAsync(user);
                foreach (var role in UserRoles)
                {
                    claims.Add(new Claim(ClaimTypes.Role, role));
                }
                #endregion

                // Create claims identity
                var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

                // Create claims principal
                var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);

                // Sign in with the claims
                await _signInManager.SignInAsync(user, isPersistent: loginDto.RememberMe);


                #region Determine UserType and redirect

                if (user.UserType == UserTypeEnum.Student)
                {
                    response.RedirectUrl = "/Student/Index";
                }
                else if (user.UserType == UserTypeEnum.Instructor)
                {
                    var instructor = user as OnlineQuiz.DAL.Data.Models.Instructor;

                    if (instructor != null)
                    {
                        if (instructor.Status == ApprovalStatus.Pending)
                        {
                            response.Errors.Add("Instructor approval is pending.");
                            return response;
                        }
                        else if (instructor.Status == ApprovalStatus.Denied)
                        {
                            response.Errors.Add("Your account has been denied by the admin.");
                            return response;
                        }
                    }

                    response.RedirectUrl = "/Instructor/DashBoared";
                }
                else if (user.UserType == UserTypeEnum.Admin)
                {
                    response.RedirectUrl = "/Admin/DashBoard";
                } 
                #endregion


                response.successed = result.Succeeded;
                return response;

            }
            response.Errors.Add("Wrong Password or Email");
            response.PropName = nameof(loginDto.Password);
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

     public async Task<GeneralRespnose> ResendConfirmationEmail(string userId, IUrlHelper Url)
        {
            var response = new GeneralRespnose();

      
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                response.Errors.Add("User not found.");
                return response;
            }

         
            var emailConfirmationToken = await _userManager.GenerateEmailConfirmationTokenAsync(user);

    
            var confirmationLink = Url.Action("ConfirmEmail", "Home",
                new { userId = user.Id, token = emailConfirmationToken },
                "https"); 

       
            var confirmationEmailBody = $"Dear {user.UserName},\n\n" +
                "To complete your registration, please confirm your email address by clicking the link below:\n" +
                $"{confirmationLink}\n\n" +
                "If you did not create an account, please ignore this email.\n\n" +
                "Best regards,\n" +
                "[Online Quiz Platform]\n" +
                "[+20 155 134 9812]";

            // Send confirmation email
            var emailResult = await _emailService.SendEmailAsync(user.Email, "Confirm Your Email Address", confirmationEmailBody);
            if (!emailResult.successed)
            {
                response.Errors.AddRange(emailResult.Errors);
                return response;
            }

            response.successed = true;
            return response;
        }

     public async Task<GeneralRespnose> ForgotPassword( ForgotPasswordDto forgotPasswordDto , IUrlHelper urlHelper)
        {
            var response = new GeneralRespnose();   
            var user = await _userManager.FindByEmailAsync(forgotPasswordDto.Email);
            if(user == null)
            {
                response.Errors.Add("Email not found. Please make sure the email is correct.");
                response.PropertyName = nameof(forgotPasswordDto.Email);
                return (response);
            }

            #region ResetPaswword

            //  Reset  and Encode password token

            var token = WebUtility.UrlEncode(await _userManager.GeneratePasswordResetTokenAsync(user)); 

            //  password reset link
            var resetLink = urlHelper.Action("ResetPassword", "Home",
                new { token , email = user.Email }, "https");

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





        public async Task<GeneralRespnose> AddRole(string RoleName )
        {
            var response = new GeneralRespnose();

            var roleExists = await _roleManager.RoleExistsAsync(RoleName);
            if (roleExists)
            {
                response.Errors.Add("Role already exists.");
                return response;
            }

            var roleResult = await _roleManager.CreateAsync(new CustomRole { Name = RoleName});
            if (roleResult.Succeeded)
            {
                response.successed = true;
                return response;
            }

            response.Errors.AddRange(roleResult.Errors.Select(e => e.Description));
            return response;
        }

        public async Task<GeneralRespnose> DeleteRole(string RoleId)
        {
            var response = new GeneralRespnose();

            var role = await _roleManager.FindByIdAsync(RoleId);
            if (role == null)
            {
                response.Errors.Add("Role not found.");
                return response;
            }

            role.IsDeleted = true;
            var roleResult = await _roleManager.UpdateAsync(role);
      
            if (roleResult.Succeeded)
            {
             
                response.successed = true;
                return response;
            }
            response.Errors.AddRange(roleResult.Errors.Select(e => e.Description));
            return response;
        }

        public async Task<GeneralRespnose> RestoreRole(string RoleId)
        {
            var response = new GeneralRespnose();

          var role =  _roleManager.Roles
                .Where(r => r.IsDeleted && r.Id == RoleId) 
                    .FirstOrDefault();
            if (role == null)
            {
                response.Errors.Add("Role not found or already active.");
                return response;
            }
            role.IsDeleted = false;
            var result = await _roleManager.UpdateAsync(role);
            if (result.Succeeded)
            {
                response.successed = true;
                return response;
            }
            response.Errors.AddRange(result.Errors.Select(e => e.Description));
            return response;
        }

        public async Task<GeneralRespnose> AddRoleToUser(string UserId, string RoleId)
        {
            var response = new GeneralRespnose();

            var user = await _userManager.FindByIdAsync(UserId);
            if (user == null)
            {
                response.Errors.Add("User not found.");
                return response;
            }
            var role = await _roleManager.FindByIdAsync(RoleId);
            if (role == null || role.IsDeleted)
            {
                response.Errors.Add("Role does not exist or is deleted.");
                return response;
            }


            var result = await _userManager.AddToRoleAsync(user, role.Name);
            if (result.Succeeded)
            {
                response.successed = true;
                return response;             
            }
            response.Errors.AddRange(result.Errors.Select(e => e.Description));
            return response;
        }

        public async Task<GeneralRespnose> RemoveRoleFromUser(string UserId, string RoleId)
        {
            var response = new GeneralRespnose();

            var user = await _userManager.FindByIdAsync(UserId);
            if (user == null)
            {
                response.Errors.Add("User not found.");
                return response;
            }
            var role = await _roleManager.FindByIdAsync(RoleId);
            if (role == null || role.IsDeleted)
            {
                response.Errors.Add("Role does not exist or is deleted.");
                return response;
            }

            var isInRole = await _userManager.IsInRoleAsync(user, role.Name);
            if (!isInRole)
            {
                response.Errors.Add("User is not in this role.");
                return response;
            }

           
            var result = await _userManager.RemoveFromRoleAsync(user, role.Name);
            if (result.Succeeded)
            {
                response.successed = true;
                return response;           
            }
            response.Errors.AddRange(result.Errors.Select(e => e.Description));
            return response;
        }

        public async Task<List<GetAllRolesDto>> GetAllRoles()
        {
            var roles =  _roleManager.Roles
                .Where(r => !r.IsDeleted) 
                .Select(r => new GetAllRolesDto
                {
                    RoleId = r.Id,
                    RoleName = r.Name
                })
                .AsNoTracking().ToList(); 

            return roles; 
        }

        public async Task<List<GetAllRolesDto>> GetAllRolesIsDeleted()
        {
            var roles =  _roleManager.Roles
                .Where(r => r.IsDeleted)
                .Select(r => new GetAllRolesDto
                {
                    RoleId = r.Id,
                    RoleName = r.Name
                })
                .AsNoTracking().ToList(); 

            return roles;
        }

        public async Task<RoleResponce<UserRoleInfo>> GetUsersInRole(string RoleId)
        {
            var response = new RoleResponce<UserRoleInfo>();

            var role = await _roleManager.FindByIdAsync(RoleId);
            if (role == null)
            {
                response.Errors.Add("Role does not exist.");
                return response;
            }

            // Get users in the specified role
            var users = await _userManager.GetUsersInRoleAsync(role.Name);
            var userCount = users.Count();

            // Prepare the response data
            response.Data = new UserRoleInfo
            {
                RoleName = role.Name,
                UsersCount = userCount,
                Users = users.Select(u => new UserInfo
                {
                    UserId = u.Id,
                    UserName = u.UserName,
                    Email = u.Email
                }). ToList()
              
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

        public async Task<List<UserTypeEnum>> GetAllUserTypes()
        {
            return await Task.FromResult(Enum.GetValues(typeof(UserTypeEnum)).Cast<UserTypeEnum>().ToList());
        }

        public async Task<List<GenderType>> GetAllGenderTypes()
        {
            return await Task.FromResult(Enum.GetValues(typeof(GenderType)).Cast<GenderType>().ToList());
        }

        public async Task<string> GetUserIdByGmail(string gmail)
        {
            if (string.IsNullOrEmpty(gmail))
            {
                throw new ArgumentException("Email cannot be null or empty", nameof(gmail));
            }

        
            var user = await _userManager.FindByEmailAsync(gmail);

            if (user == null)
            {
                return null;
            }

            return user.Id; 
        }

        public async Task Logout()
        {
           await _signInManager.SignOutAsync(); 
        }
    }

}




