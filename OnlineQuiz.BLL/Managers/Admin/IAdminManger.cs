using OnlineQuiz.BLL.Dtos.Admin.InstructorDtos;
using OnlineQuiz.BLL.Dtos.Admin.StudentDtos;
using OnlineQuiz.DAL.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineQuiz.BLL.Managers.Admin
{
    public interface IAdminManger
    {  
        
        // get all  student 
        IEnumerable<StudentReadDto> GetAllStudentAsync();

        // get by id or name student 
        StudentReadDto GetStudentById(string id);
        StudentReadDto GetStudentByName(string name);


        // add student 
        public void AddStudent(StudentAddDto StudentAddDto);
        // edit student 
        public void UpdateStudent(StudentUpdateDto StudentUpdateDto);

        // delete student 
        public void DeleteStudent(string id);

        //--------------------------------------------------------------------------------

        // get all instractour 
        IEnumerable<InstructorReadDto> GetAllInstructo();

        // get by id or name instractour
        InstructorReadDto GetInstructorById(string id);
        InstructorReadDto GetInstructorByName(string name);


        // add instractour  
        public void AddInstructor(InstructorAddDto InstructorAddDto);
        // edit instractour 
        public void UpdateInstructor(IstrurctorUpdateDto IstrurctorUpdateDto);

        // delete instractour 
        public void DeleteInstructor(string id);


        //--------------------------------------------------------------------------


        // Approve and deny instractour after login only accses for admin

        string ApproveInstructorAsync(string id);
        string DenyInstructorAsync(string id);

        // ban and un ban users accses for admin
        void BanUserAsync(string studentId);
        void UnbanUserAsync(string studentId);


        // App details instractour 

        string NumOfStudent();
        string NumOfInstructor();
        string NumOfQuizes();
        string NumOfAttempes();


        // role admin only acsses

        void SaveChanges();
    }
}
