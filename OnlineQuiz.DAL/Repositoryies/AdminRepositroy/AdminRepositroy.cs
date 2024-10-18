using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using OnlineQuiz.DAL.Data.DBHelper;
using OnlineQuiz.DAL.Data.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineQuiz.DAL.Repositoryies.AdminRepositroy
{
    public class AdminRepositroy : IAdminRepositroy
    {
        private readonly QuizContext _Context;

        public AdminRepositroy(QuizContext QuizContext)
        {
            _Context = QuizContext;
        }
        public void AddInstructor(Instructor Instructor)
        {
            _Context.Add(Instructor);
            _Context.SaveChanges();
        }

        public void AddStudent(Student student)
        {
            _Context.Add(student);
            SaveChanges();
        }


        
        public void DeleteInstructor(Instructor Instructor)
        {
            _Context.Remove(Instructor);
             SaveChanges();
        }

        public void DeleteStudent(Student student)
        {
            _Context.Remove(student);
            SaveChanges();
        }

        public IEnumerable<Instructor> GetAllInstructo()
        {
            return _Context.Instructors.ToList();
        }

        public IEnumerable<Student> GetAllStudentAsync()
        {
             
            return  _Context.Students.ToList();
        }

        public Instructor GetInstructorById(string id)
        {
            return _Context.Instructors.FirstOrDefault(Key => Key.Id == id.ToString());
        }

        public Instructor GetInstructorByName(string name)
        {
            return _Context.Instructors.FirstOrDefault(Key => Key.UserName == name);

        }

        public Student GetStudentById(string id)
        {
            return  _Context.Students.FirstOrDefault(Key => Key.Id == id);

        }

        public Student GetStudentByName(string name)
        {
            return  _Context.Students.FirstOrDefault(Key => Key.UserName == name.ToString());

        }

      
      

        public void UpdateInstructor(Instructor Instructor)
        {
             SaveChanges();
        }

        public void UpdateStudent(Student student)
        {
            SaveChanges();
        }




        public void SaveChanges()
        {
             _Context.SaveChanges();
        }

        public void ApproveInstructorAsync(string InstructorId)
        {
            var instructor = _Context.Instructors.FirstOrDefault(key=> key.Id == InstructorId);
            if (instructor != null)
            {
                instructor.Status = ApprovalStatus.Approved;
                _Context.Update(instructor);
                _Context.SaveChanges();
            }
        }


        public void DenyInstructorAsync(string InstructorId)
        {
            var instructor =  _Context.Instructors.FirstOrDefault(key => key.Id == InstructorId);
            if (instructor != null)
            {
                instructor.Status = ApprovalStatus.Denied;
                _Context.Update(instructor);
                _Context.SaveChanges();
            }
        }



        public void BanUserAsync(string UserId)
        {
            var users = _Context.users.FirstOrDefault(key => key.Id == UserId);
            users.IsBanned = true;
            _Context.Update(users);
            _Context.SaveChanges();

        }

        public void UnbanUserAsync(string UserId)
        {
            var users =  _Context.users.FirstOrDefault(key => key.Id == UserId);
            users.IsBanned = false;
            _Context.Update(users);
            _Context.SaveChanges();
        }

        public Users GetUsertById(string id)
        {
           return _Context.users.FirstOrDefault(key => key.Id == id);
        }

        public string NumOfStudent()
        {
            return _Context.Students.Count().ToString();
        }

        public string NumOfInstructor()
        {
            return  _Context.Instructors.Count().ToString();
        }

        public string NumOfQuizes()
        {
            return _Context.quizzes.Count().ToString();
        }

        public string NumOfAttempes()
        {
             return _Context.attempts.Count().ToString();
        }


    }
}
