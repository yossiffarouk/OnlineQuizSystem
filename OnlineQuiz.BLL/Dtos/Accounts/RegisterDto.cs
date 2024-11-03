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
       
         
        [StringLength(80, ErrorMessage = "Username must be between 5 and 30 characters", MinimumLength = 5)]
        [Required(ErrorMessage = "Username is required")]
        [RegularExpression(@"^[a-zA-Z0-9\s,.-]+$", ErrorMessage = "Name can only contain letters, numbers, spaces, commas, dots, and hyphens.")]
        public string UserName { get; set; }

            [Required(ErrorMessage = "Email is required")]
            [DataType(DataType.EmailAddress)]
            [EmailAddress(ErrorMessage = "Invalid Email Format")]
            public string Email { get; set; }



            [DataType(DataType.Password)]
            [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)[A-Za-z\d@$!%*?&]{5,}$",
            ErrorMessage = "Password must contain at least one uppercase letter, one lowercase letter, and one number.")]
           [StringLength(100, ErrorMessage = "Password must be at least 6 characters long", MinimumLength = 6)]
           [Required(ErrorMessage = "Password is required")]
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

        [Required(ErrorMessage = "Address is required")]
        [RegularExpression(@"^[a-zA-Z0-9\s,.-]+$", ErrorMessage = "Address can only contain letters, numbers, spaces, commas, dots, and hyphens.")]
        public string Address { get; set; } 


           
        }

    
   
}