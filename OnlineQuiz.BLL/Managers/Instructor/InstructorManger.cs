using AutoMapper;
using OnlineQuiz.BLL.Dtos.Admin.InstructorDtos;
using OnlineQuiz.BLL.Dtos.Admin.StudentDtos;
using OnlineQuiz.BLL.Dtos.Instructor;
using OnlineQuiz.DAL.Data.Models;
using OnlineQuiz.DAL.Repositoryies.InstructorRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineQuiz.BLL.Managers.Instructor
{
    public class InstructorManger : IInstructorManger
    {
        private readonly IInstructorRepository _iInstructorRepository;

        public IMapper _mapper { get; }

        public InstructorManger(IInstructorRepository IInstructorRepository, IMapper mapper)
        {
            _iInstructorRepository = IInstructorRepository;
            _mapper = mapper;
        }






        public IEnumerable<InstructorSeeQuizDto> ShowScoresOfQuiz(int quizId)
        {
            var result = _iInstructorRepository.showAttempts(quizId);

            return result.Select(x => new InstructorSeeQuizDto { id = x.StudentId, Name = x.Student.UserName, Score = x.Score, Id = x.QuizId }).ToList();

        }

        public InstructorReadDto GetInsById(string id)
        {
            return _mapper.Map<InstructorReadDto>(_iInstructorRepository.GetInsById(id));
        }



        // adding student to instructor
        public async Task AddStudentToInstructorAsync(string studentId, string instructorId)
        {
            await _iInstructorRepository.AddStudentToInstructorAsync(studentId, instructorId);
        }

        // removing student from instructor
        public async Task RemoveStudentFromInstructorAsync(string studentId, string instructorId)
        {
            await _iInstructorRepository.RemoveStudentFromInstructorAsync(studentId, instructorId);
        }

        public string UpdateInstructorProfile(string id, UpdateInstructorrProfileDto UpdateInstructorrProfileDto)
        {
            var instructor = _iInstructorRepository.GetInstructorById(id);

            if (instructor == null)
            {
                return "instructor not found ";
            }

          

            _iInstructorRepository.UpdateInstructorProfile(_mapper.Map<OnlineQuiz.DAL.Data.Models.Instructor>(UpdateInstructorrProfileDto));
            return "Instructor updated successfully";
        }
    }
}
