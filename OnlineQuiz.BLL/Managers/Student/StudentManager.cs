
using AutoMapper;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using OnlineQuiz.BLL.Dtos.StudentDtos;
using OnlineQuiz.BLL.Wrapper;
using OnlineQuiz.DAL.Data.Models;
using OnlineQuiz.DAL.Repositoryies.Base;
using OnlineQuiz.DAL.Repositoryies.StudentReposatory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineQuiz.BLL.Managers.Student
{
    public class StudentManager : IStudentManager
    {
        private readonly IStudentRepo _studentRepo;
        private readonly IMapper _mapper;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public StudentManager(IStudentRepo studentRepo, IMapper mapper , IWebHostEnvironment webHostEnvironment)
        {
            _studentRepo = studentRepo;
            _mapper = mapper;
            _webHostEnvironment = webHostEnvironment;
        }





        public string  UploadProfileImageAsync(IFormFile profileImage, string userId)
        {
            if (profileImage == null || profileImage.Length == 0)
            {
                throw new ArgumentException("Please select a valid image file.");
            }

            // Define the path where the image will be saved
            var uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, "images");
            Directory.CreateDirectory(uploadsFolder); // Ensure the folder exists

            var uniqueFileName = Guid.NewGuid().ToString() + Path.GetExtension(profileImage.FileName);
            var filePath = Path.Combine(uploadsFolder, uniqueFileName);

            // Save the image file
            using (var fileStream = new FileStream(filePath, FileMode.Create))
            {
                 profileImage.CopyToAsync(fileStream);
            }

            // Update ImageUrl in the database
            var student =  _studentRepo.GetById(userId); // Adjust if `GetByIdAsync` exists in IStudentRepo
            if (student == null)
            {
                throw new Exception("Student not found.");
            }

            student.ImgUrl = "/images/" + uniqueFileName;
            _studentRepo.Update(student);
             _studentRepo.SaveChangesAsync(); // Ensure SaveChangesAsync is awaited

            return student.ImgUrl;
        }







        public IQueryable<StudentReadDto> GetAll()
        {
            return _mapper.ProjectTo<StudentReadDto>(_studentRepo.GetAll());
        }
        public StudentReadDto GetById(string id)
        {
            var student = _mapper.Map<StudentReadDto>(_studentRepo.GetById(id));
            return student;
        }
        public void Add(StudentAddDto Studententity)
        {
            var student = _mapper.Map<OnlineQuiz.DAL.Data.Models.Student>(Studententity);
            _studentRepo.Add(student);
        }
        public void Update(StudentUpdateDto Studententity)
        {
            var StudentModel = _studentRepo.GetById(Studententity.Id);
            _studentRepo.Update
                (_mapper.Map<StudentUpdateDto, OnlineQuiz.DAL.Data.Models.Student>(Studententity, StudentModel));
        }
        public void DeleteById(string id)
        {
            _studentRepo.DeleteById(id);
        }
        public IQueryable<StudentReadDto> GetStudentsByGrade(string grade)
        {
            return _mapper.ProjectTo<StudentReadDto>(_studentRepo.GetStudentsByGrade(grade));
        }
        public StudentDetailesDto GetByIdWithDetails(string studentId)
        {
            var student = _studentRepo.GetByIdWithDetails(studentId);
            if (student == null)
                return null;
            var studentDto = _mapper.Map<StudentDetailesDto>(student);
            return studentDto;

        }

        public async Task<PagintedResult<StudentReadPaginatedDto>> GetPaginatedStudentsAsync(int pageNumber, int pageSize)
        {
            var Query = _mapper.ProjectTo<StudentReadPaginatedDto>(_studentRepo.GetAll());
            return await QuerableExtentions.ToPagintedListAsync(Query, pageNumber, pageSize);
        }

        public IEnumerable<StudentReadDto> GetStudentsWithInstructor(string instructorId)
        {
            var student = _studentRepo.GetStudentsWithInstructor(instructorId);
            var ReadDto = student.Select(x => new StudentReadDto
            {
                Id = x.Student.Id,
                UserName = x.Student.UserName,
                Email = x.Student.Email,
                PhoneNumber = x.Student.PhoneNumber,
                Grade = x.Student.Grade,
                ImgUrl = x.Student.ImgUrl

            });
            return ReadDto;
        }

        public IEnumerable<StudentReadDto> GetStudentsToAdd(string instructorId)
        {
            return _mapper.Map<IEnumerable<StudentReadDto>>(_studentRepo.GetStudentsToAdd(instructorId));
        }

        public async Task<int> SaveChangesAsync()
        {
            return await _studentRepo.SaveChangesAsync();
        }
    }
}
