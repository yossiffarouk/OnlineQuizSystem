
using AutoMapper;
using OnlineQuiz.BLL.Dtos.StudentDtos;
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
    public class StudentManager :IStudentManager
    {
        private readonly IStudentRepo _studentRepo;
        private readonly IMapper _mapper;
        public StudentManager(IStudentRepo studentRepo,IMapper mapper)
        {
            _studentRepo = studentRepo;
            _mapper = mapper;
        }
        public IEnumerable<StudentReadDto> GetAll()
        {
          return  _mapper.Map<List<StudentReadDto>>(_studentRepo.GetAll());
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
                (_mapper.Map<StudentUpdateDto,OnlineQuiz.DAL.Data.Models.Student>
                (Studententity,StudentModel));
        }
        public void DeleteById(string id)
        {
             _studentRepo.DeleteById(id);
        }
        public IEnumerable<StudentReadDto> GetStudentsByGrade(string grade)
        {
            return _mapper.Map<List<StudentReadDto>>(_studentRepo.GetStudentsByGrade(grade));
        }
        public StudentDetailesDto GetByIdWithDetails(string studentId)
        {
            var student =  _studentRepo.GetByIdWithDetails(studentId); 

            if (student == null)
                return null;
            // Map to DTO
            var studentDto = _mapper.Map<StudentDetailesDto>(student);
            return studentDto;
        }
    }
}
