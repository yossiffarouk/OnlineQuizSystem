using Azure;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Routing;
using OnlineQuiz.BLL.Dtos.Accounts;
using OnlineQuiz.BLL.Managers.Accounts;
using OnlineQuiz.DAL.Data.Models;

namespace OnlineQuiz.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize(Roles = Roles.Student)]
    public class AccountsController : ControllerBase
    {
        private readonly IAccountManager _accountManager;

        public AccountsController(IAccountManager accountManager)
        {
            _accountManager = accountManager;
        }
        [HttpPost("Register")]
        public async Task<ActionResult> Register(RegisterDto registerDto)
        {


            if (!ModelState.IsValid)
            {
                return BadRequest(new { Errors = "Invalid input data" });
            }


            // This provides the UrlHelper instance
            var urlHelper = Url;

            // Pass the urlHelper to the Register method
            var response = await _accountManager.Register(registerDto, urlHelper);

            if (response.successed)
            {

                // return CreatedAtAction(nameof(Register), response);
                return Ok(new { message = "Register successful" });

            }


            return BadRequest(response.Errors);
        }

        [HttpPost("Login")]
        public async Task<ActionResult> Login(LoginDto loginDto)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest(new { Errors = "Invalid input data" });
            }

            var response = await _accountManager.Login(loginDto);

            if (response.successed)
            {
             return Ok(new { message = "Login successful" });
            }

            // Return unauthorized if login failed
            return Unauthorized(response.Errors);
        }

        [HttpGet("ConfirmEmail")]
        public async Task<IActionResult> ConfirmEmail(string userId, string token)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new { Errors = "Invalid input data" });
            }
            var responce = await _accountManager.ConfirmEmail(userId, token);
            if (responce.successed)
                
            {
                return Ok(new { message = "Email confirmed successfully" });
            }
            return BadRequest(responce.Errors);
        }

        [HttpPost("ForgotPassword")]
        public async Task<IActionResult> ForgotPassword(ForgotPasswordDto forgotPasswordDto )
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new { Errors = "Invalid input data" });
            }

            var UrlHepler = Url;
            var result = await _accountManager.ForgotPassword(forgotPasswordDto , UrlHepler);
            if(result.successed)
            {
                return Ok(new { message = "Password reset email sent successfully. Please check your inbox." });
            }
            return BadRequest(result.Errors);
        }

        [HttpPost("ResetPassword")]
        public async Task<IActionResult> ResetPassword(ResetPasswordDto resetPasswordDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new { Errors = "Invalid input data" });
            }

            var UrlHepler = Url;
            var result = await _accountManager.ResetPassword(resetPasswordDto);
            if (result.successed)
            {
                return Ok(new { message = "Your password has been reset successfully." });
            }
            return BadRequest(result.Errors);
        }
        [HttpGet("ShowResetToken")]
        public IActionResult ShowResetToken(string token, string email)
        {

            return Ok(new
            {

                Token = token,


                Email = email
            });
        }

        [HttpPost("AddRole")]
        public async Task<IActionResult> AddRole( string RoleName)
        {
            var result = await _accountManager.AddRole(RoleName);
            if (!result.successed)
            {
                return BadRequest(result.Errors);
            }
            return Ok(result.successed);
        }

        // Delete an existing role
        [HttpDelete("DeleteRole")]
        public async Task<IActionResult> DeleteRole(string RoleName)
        {
            var result = await _accountManager.DeleteRole(RoleName);
            if (!result.successed)
            {
                return BadRequest(result.Errors);
            }
            return Ok(result.successed);
        }

        // Add a role to a user
        [HttpPost("AddRoleToUser")]
        public async Task<IActionResult> AddRoleToUser(string UserId,  string RoleName)
        {
            var result = await _accountManager.AddRoleToUser(UserId, RoleName);
            if (!result.successed)
            {
                return BadRequest(result.Errors);
            }
            return Ok(result.successed);
        }

        // Remove a role from a user
        [HttpDelete("RemoveRoleFromUser")]
        public async Task<IActionResult> RemoveRoleFromUser(string UserId, string RoleName)
        {
            var result = await _accountManager.RemoveRoleFromUser(UserId, RoleName);
            if (!result.successed)
            {
                return BadRequest(result.Errors);
            }
            return Ok(result.successed);
        }

        // Get all roles
        [HttpGet("GetAllRoles")]
        public async Task<IActionResult> GetAllRoles()
        {
            var result = await _accountManager.GetAllRoles();
            return Ok(result.Data);
        }

        // Get all users in a specific role
        [HttpGet("GetUsersInRole")]
        public async Task<IActionResult> GetUsersInRole(string RoleName)
        {
            var result = await _accountManager.GetUsersInRole(RoleName);
            if (!result.successed)
            {
                return BadRequest(result.Errors);
            }
            return Ok(result.Data);
        }
    }
}
