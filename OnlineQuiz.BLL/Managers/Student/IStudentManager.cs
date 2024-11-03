using Microsoft.AspNetCore.Http;
using OnlineQuiz.BLL.Dtos.StudentDtos;
using OnlineQuiz.BLL.Wrapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineQuiz.BLL.Managers.Student
{
    public interface IStudentManager 
    {
        IQueryable<StudentReadDto> GetAll();
        IEnumerable<StudentReadDto> GetStudentsWithInstructor(string instructorId);
        StudentReadDto GetById(string id);
        void Add(StudentAddDto Studententity);
        void Update(StudentUpdateDto Studententity);
        void DeleteById(string id);
        IQueryable<StudentReadDto> GetStudentsByGrade(string grade);
        public StudentDetailesDto GetByIdWithDetails(string studentId);
        public Task<PagintedResult<StudentReadPaginatedDto>> GetPaginatedStudentsAsync(int pageNumber, int pageSize);
        IEnumerable<StudentReadDto> GetStudentsToAdd(string instructorId);
       string UploadProfileImageAsync(IFormFile profileImage, string userId);
        Task<int> SaveChangesAsync();

    }
}
