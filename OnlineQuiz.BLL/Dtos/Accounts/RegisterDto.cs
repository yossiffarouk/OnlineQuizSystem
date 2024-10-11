using OnlineQuiz.DAL.Data.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineQuiz.BLL.Dtos.Accounts
{
    public class RegisterDto
    {
       
            [Required(ErrorMessage = "Username is required")]
            [StringLength(80, ErrorMessage = "Username must be between 5 and 80 characters", MinimumLength = 5)]
            public string UserName { get; set; }

            [Required(ErrorMessage = "Email is required")]
            [DataType(DataType.EmailAddress)]
            [EmailAddress(ErrorMessage = "Invalid Email Format")]
            public string Email { get; set; }

            [Required(ErrorMessage = "Password is required")]
            [DataType(DataType.Password)]
            [StringLength(100, ErrorMessage = "Password must be at least 6 characters long", MinimumLength = 6)]
            public string Password { get; set; }

            [Required(ErrorMessage = "Password confirmation is required")]
            [DataType(DataType.Password)]
            [Compare("Password", ErrorMessage = "Password and confirmation do not match")]
            public string ConfirmedPassword { get; set; }

            [Required(ErrorMessage = "UserType is required")]
            public UserTypeEnum UserType { get; set; } = UserTypeEnum.Student;

            [Required(ErrorMessage = "Age is required")]
            [Range(5, 120)]
            public int Age { get; set; }

        [Required(ErrorMessage = "Gender is required")]
        public GenderType Gender { get; set; } 

        public string Address { get; set; } = "Egypt";


           
        }

    
   
}