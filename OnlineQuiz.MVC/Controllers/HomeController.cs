using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.Routing;
using OnlineQuiz.BLL.Dtos.Accounts;
using OnlineQuiz.BLL.Dtos.Accounts.VM;
using OnlineQuiz.BLL.Managers.Accounts;
using OnlineQuiz.DAL.Data.Models;
using OnlineQuiz.MVC.Models;
using System.Diagnostics;

namespace OnlineQuiz.MVC.Controllers
{
    public class HomeController : Controller
    {
        private readonly IAccountManager _accountManager;

        public HomeController(IAccountManager accountManager)
        {
            _accountManager = accountManager;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }







        [HttpGet]
        public IActionResult Login()
        {
            var loginDto = new LoginDto();
            return View("Login" , loginDto);
        }


        [HttpPost]
        public async Task<ActionResult> Login(LoginDto loginDto)
        {
            if (!ModelState.IsValid)
            {
                return View(loginDto);
            }
            var response = await _accountManager.LoginMVC(loginDto);

            if (response.successed)
            {
                return Redirect(response.RedirectUrl);
            }
            foreach (var error in response.Errors)
            {
                ModelState.AddModelError(response.PropName, error);
            }

            return View(loginDto);
        }











        [HttpGet]
        public async Task<IActionResult> Register()
        {

            await PopulateViewBags();
            var registerModel = new RegisterDto(); 
            return View("Register", registerModel);
        
        }

        [HttpPost]
        public async Task<ActionResult> Register(RegisterDto registerDto)
        {
          
            if (!ModelState.IsValid)
            {
                await PopulateViewBags(); 
                return View(registerDto);
            }

            
            var response = await _accountManager.Register(registerDto, Url);

      
            if (!response.successed)
            {
              
                foreach (var error in response.Errors)
                {
                    ModelState.AddModelError(response.PropertyName, error.ToString());
                }

                await PopulateViewBags(); 
                return View(registerDto); 
            }

            // Registration succeeded, proceed to email verification
            var userId = await _accountManager.GetUserIdByGmail(registerDto.Email);
            if (!string.IsNullOrEmpty(userId))
            {
                return RedirectToAction("VerifyEmail", new { UserId = userId });
            }

            // If the userId was not found, return a generic error message
            ModelState.AddModelError(string.Empty, "User ID not found. Please try again.");
            await PopulateViewBags();
            return View(registerDto);
        }


        [HttpGet("ConfirmEmail")]
        public async Task<IActionResult> ConfirmEmail(string userId, string token)
        {

            var responce = await _accountManager.ConfirmEmail(userId, token);
            if (responce.successed)
            {
                ViewBag.Message = "Email confirmed successfully!";
            }
            else
            {
                ViewBag.Message = "Email confirmation failed: " + string.Join(", ", responce.Errors);
            }

            return View("EmailConfirmed"); 
        }

  
        [HttpGet]
        public IActionResult VerifyEmail(string userId)
        {
            if (string.IsNullOrEmpty(userId))
            {
                return BadRequest("User ID is required.");
            }

            return View(new VerifyEmailVM { UserId = userId });
        }

        [HttpPost]
        public async Task<IActionResult> ResendConfirmationEmail(string userId)
        {
            var UrlHelper = Url;
            var response = await _accountManager.ResendConfirmationEmail(userId , UrlHelper);
            if (!response.successed)
            {
                return BadRequest(response.Errors);
            }

            string message = "New confirmation email has been sent. Please check your inbox.";
            return View("VerifyEmail", new VerifyEmailVM { UserId = userId  , Message = message }); 
        }


        [HttpGet]
        public IActionResult ForgotPassword()
        {
            var forgotPasswordDto = new ForgotPasswordDto();
            return View("ForgotPassword", forgotPasswordDto);
        }

        [HttpPost]
        public async Task<IActionResult> ForgotPassword(ForgotPasswordDto forgotPasswordDto)
        {
            if (!ModelState.IsValid)
            {
                return View(forgotPasswordDto);
            }

            var result = await _accountManager.ForgotPassword(forgotPasswordDto, Url);
            if (result.successed)
            {

                ViewBag.Message = "Password reset email sent successfully. Please check your inbox.";
                return View("ForgotPassword");
            }

            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(result.PropertyName, error);
            }

            return View(forgotPasswordDto);
        }

        [HttpGet]
        public IActionResult ResetPassword(string token, string email)
        {

            if (string.IsNullOrEmpty(token) || string.IsNullOrEmpty(email))
            {
                ViewBag.Message = "Invalid token or email. Please try again.";
                return RedirectToAction("ForgotPassword");
            }


            var resetPasswordDto = new ResetPasswordDto
            {
                Token = token,
                Email = email
            };

            return View(resetPasswordDto);
        }


        [HttpPost]
        public async Task<IActionResult> ResetPassword(ResetPasswordDto resetPasswordDto)
        {
            if (!ModelState.IsValid)
            {

                return View(resetPasswordDto);
            }

            var result = await _accountManager.ResetPassword(resetPasswordDto);
            if (result.successed)
            {

                ViewBag.Message = "Your password has been reset successfully.";
                return View(resetPasswordDto);
            }


            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error);
            }

            return View(resetPasswordDto);
        }



        [HttpGet]
        public async Task<ActionResult> Logout()
        {
            await _accountManager.Logout();
            return RedirectToAction("Login");
        }
















        private async Task PopulateViewBags()
        {
            var userTypes = await _accountManager.GetAllUserTypes();
            var genderTypes = await _accountManager.GetAllGenderTypes();
            ViewBag.UserType = new SelectList(userTypes.Select(ut => new { Value = (int)ut, Text = ut.ToString() }), "Value", "Text");
            ViewBag.GenderType = new SelectList(genderTypes.Select(gt => new { Value = (int)gt, Text = gt.ToString() }), "Value", "Text");
        }
    }
    }

