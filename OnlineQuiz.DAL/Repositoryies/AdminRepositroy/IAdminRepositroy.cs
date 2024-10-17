using OnlineQuiz.DAL.Data.Models;
using OnlineQuiz.DAL.Repositoryies.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineQuiz.DAL.Repositoryies.AdminRepositroy
{
    public interface IAdminRepositroy
    {

        // get user for ban and un ban method  
        Users GetUsertById(string id);

        //------------------------------------------------------------------------------------

        // get all student 
        Task<IEnumerable<Student>> GetAllStudentAsync();

        // get by id or name (student)
        Task<Student> GetStudentById(string id);
        Task<Student> GetStudentByName(string name);


        // add student 
        public Task AddStudent(Student student);
        // edit student 
        public Task UpdateStudent(Student student);

        // delete student 
        public Task DeleteStudent(Student student);

        //------------------------------------------------------------------------------------

        // get all instractour 
        IEnumerable<Instructor> GetAllInstructo();

        // get by id or name instractour) 
        Instructor GetInstructorById(string id);
        Instructor GetInstructorByName(string name);


        // add instractour  
        public void AddInstructor(Instructor Instructor);
        // edit instractour 
        public void UpdateInstructor(Instructor Instructor);

        // delete instractour 
        public void DeleteInstructor(Instructor Instructor);





        // Approve and deny instractour after login only accses for admin

        void ApproveInstructorAsync(string instructorId);
        void DenyInstructorAsync(string instructorId);

        // ban and un ban users accses for admin
        void BanUserAsync(string studentId);
        void UnbanUserAsync(string studentId);


        // App details some details

        string NumOfStudent();
        string NumOfInstructor();
        string NumOfQuizes();
        string NumOfAttempes();

        // role admin only acsses

        Task SaveChanges();
       
    }
}
