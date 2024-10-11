
using Microsoft.AspNetCore.Identity;
using OnlineQuiz.DAL.Data.Configuration;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineQuiz.DAL.Data.Models
{
    //Table By Hiercay
    public enum UserTypeEnum
    {
        Student = 1,
        Instructor = 2,
        Admin = 3
    }

    public enum GenderType
    {
        Male = 1,
        Female = 2

    }

    public enum ApprovalStatus
    {
        Pending,
        Approved,
        Denied
    }




    public class Users : IdentityUser
    {



        [Range(5, 120)]
        public int Age { get; set; }

        public GenderType Gender { get; set; }

        public string Adress { get; set; }

        [DataType(DataType.ImageUrl)]
        public string? ImgUrl { get; set; }

        public UserTypeEnum UserType { get; set; }




    }

    public class Student : Users
    {
        public string? Grade { get; set; }
        public ICollection<Instructor> Instructors { get; set; } = new HashSet<Instructor>();

        //Student With Attempts
        public ICollection<Attempts> Attempts { get; set; } = new HashSet<Attempts>();


    }


    public class Instructor : Users
    {

        public ApprovalStatus? Status { get; set; } = ApprovalStatus.Pending;
        public ICollection<Student> Students { get; set; } = new HashSet<Student>();

        //  Instructor with Quizzes
        public ICollection<Quizzes> quizzes { get; set; } = new HashSet<Quizzes>();

    }





}

