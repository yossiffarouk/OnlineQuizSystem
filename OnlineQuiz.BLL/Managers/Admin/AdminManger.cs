using AutoMapper;
using Microsoft.AspNet.Identity;
using OnlineQuiz.BLL.Dtos.Admin.InstructorDtos;
using OnlineQuiz.BLL.Dtos.Admin.StudentDtos;
using OnlineQuiz.DAL.Data.Models;
using OnlineQuiz.DAL.Data.Models;
using OnlineQuiz.DAL.Repositoryies.AdminRepositroy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineQuiz.BLL.Managers.Admin
{
    public class AdminManger : IAdminManger
    {
        private readonly IAdminRepositroy _IAdminRepositroy;
        public IMapper _IMapper { get; }

        public AdminManger(IAdminRepositroy IAdminRepositroy, IMapper IMapper)
        {
            _IAdminRepositroy = IAdminRepositroy;
            _IMapper = IMapper;
        }

        //STUDENT
        //GET ALL - GET BY ID - GET BY NAME - ADD UPDATE - DELETE - SAVE  w

        //INSTRACTOUR
        //GET ALL - GET BY ID - GET BY NAME - ADD UPDATE - DELETE - SAVE  w

        //MORE ADMIN FUNC
        //


        public IEnumerable<StudentReadDto> GetAllStudentAsync()
        {
           return _IMapper.Map<IEnumerable<StudentReadDto>>(_IAdminRepositroy.GetAllStudentAsync());

        }





        public StudentReadDto GetStudentById(string id)
        {
            return _IMapper.Map<StudentReadDto>( _IAdminRepositroy.GetStudentById(id));
        }

        public StudentReadDto GetStudentByName(string name)
        {
            return _IMapper.Map<StudentReadDto>(  _IAdminRepositroy.GetStudentByName(name));
        }


        public void AddStudent(StudentAddDto StudentAddDto)
        {
            _IAdminRepositroy.AddStudent(_IMapper.Map<OnlineQuiz.DAL.Data.Models.Student>(StudentAddDto));
            //SaveChanges();
        }
        public StudentUpdateDto GetStudentByIdForUpdate(string id)
        {
            return _IMapper.Map<StudentUpdateDto>(_IAdminRepositroy.GetStudentById(id));
        }
        public void UpdateStudent(StudentUpdateDto StudentUpdateDto)
        {
            _IAdminRepositroy.UpdateStudent( _IMapper.Map(StudentUpdateDto, _IAdminRepositroy.GetStudentById(StudentUpdateDto.Id)));
            // SaveChanges();
        }



        public void DeleteStudent(string id)
        {
            _IAdminRepositroy.DeleteStudent( _IAdminRepositroy.GetStudentById(id));
            //SaveChanges();
        }


        public void SaveChanges()
        {
            _IAdminRepositroy.SaveChanges();
        }
        //-------------------------------------------------
        public IEnumerable<InstructorReadDto> GetAllInstructo()
        {
            return _IMapper.Map<List<InstructorReadDto>>(_IAdminRepositroy.GetAllInstructo());
        }

        public InstructorReadDto GetInstructorById(string id)
        {
            return _IMapper.Map<InstructorReadDto>(_IAdminRepositroy.GetInstructorById(id));
        }

        public IstrurctorUpdateDto GetInstructorByIdForUpdate(string id)
        {
            return _IMapper.Map<IstrurctorUpdateDto>(_IAdminRepositroy.GetInstructorById(id));
        }
        public InstructorReadDto GetInstructorByName(string name)
        {
            return _IMapper.Map<InstructorReadDto>(_IAdminRepositroy.GetInstructorByName(name));
        }

        public void AddInstructor(InstructorAddDto InstructorAddDto)
        {
            _IAdminRepositroy.AddInstructor(_IMapper.Map<OnlineQuiz.DAL.Data.Models.Instructor>(InstructorAddDto));
            SaveChanges();
        }

        public void UpdateInstructor(IstrurctorUpdateDto IstrurctorUpdateDto)
        {
            _IAdminRepositroy.UpdateInstructor(_IMapper.Map(IstrurctorUpdateDto, _IAdminRepositroy.GetInstructorById(IstrurctorUpdateDto.Id)));
            SaveChanges();
        }

        public void DeleteInstructor(string id)
        {
            _IAdminRepositroy.DeleteInstructor(_IAdminRepositroy.GetInstructorById(id));
            SaveChanges();
        }
        //-----------------------------------------------------------------------------------------------------
        public string ApproveInstructorAsync(string instructorDto)
        {
            var instructor = _IAdminRepositroy.GetInstructorById(instructorDto);
            if (instructor == null)
            {
                return "Instructor not found";
            }

            if (instructor.Status == ApprovalStatus.Approved)
            {
                return "Instructor is already approved";
            }

           _IAdminRepositroy.ApproveInstructorAsync(instructorDto);
            return "Instructor Approved succefully";
        }

        public string DenyInstructorAsync(string id)
        {
            var instructor =  _IAdminRepositroy.GetInstructorById(id);
            if (instructor == null)
            {
               return  "Instructor not found";
            }

            if (instructor.Status == ApprovalStatus.Denied)
            {
                return "Instructor has already been denied";
            }

             _IAdminRepositroy.DenyInstructorAsync(id);
            return "Instructor denyed succefully";
        }

        public void BanUserAsync(string userId)
        {
            var user =  _IAdminRepositroy.GetUsertById(userId);
            //if (user == null)
            //{
            //    string message = "instructor not found";
            //}

            //if (user.IsBanned)
            //{
            //    string message = "instructor is already banned";
            //}

            _IAdminRepositroy.BanUserAsync(userId);
            
        }



        public void UnbanUserAsync(string userId)
        {
            var user = _IAdminRepositroy.GetUsertById(userId);
           

             _IAdminRepositroy.UnbanUserAsync(userId);
        }

        public string NumOfStudent()
        {
            return _IAdminRepositroy.NumOfStudent();
        }

        public string NumOfInstructor()
        {
            return _IAdminRepositroy.NumOfInstructor();
        }

        public string NumOfQuizes()
        {
            return _IAdminRepositroy.NumOfQuizes();
        }

        public string NumOfAttempes()
        {
            return _IAdminRepositroy.NumOfAttempes();
        }

        public IEnumerable<InstructorStatusDto> GetAllInstructorPanding()
        {
            return _IMapper.Map<List<InstructorStatusDto>>(_IAdminRepositroy.GetAllInstructorPanding());
        }
    }
}
