using AutoMapper;
using OnlineQuiz.BLL.Dtos.Question;
using OnlineQuiz.DAL.Data.Models;
using OnlineQuiz.DAL.Repositoryies.QuestionRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineQuiz.BLL.Managers.QuestionManager
{
    public class QuestionManager : IQuestionManager
    {
        private readonly IQuestionsRepository _questionsRepository;
        private readonly IMapper _mapper;

        public QuestionManager(IQuestionsRepository questionsRepository, IMapper mapper)
        {
            _questionsRepository = questionsRepository;
            _mapper = mapper;
        }

        public IQueryable<QuestionDto> GetAllQuestions()
        {
            var questions = _questionsRepository.GetAll(); 
            return _mapper.ProjectTo<QuestionDto>(questions);  // Use ProjectTo for direct mapping to DTO
        }

        public QuestionDto GetQuestionById(int id)
        {
            var question = _questionsRepository.GetById(id);
            return _mapper.Map<QuestionDto>(question);
        }

        public void AddQuestion(createQuestionDto createquestionDto)
        {
            var question = _mapper.Map<Questions>(createquestionDto);
            _questionsRepository.Add(question);
        }

        public void UpdateQuestion(QuestionDto questionDto)
        {
         var question = _mapper.Map<Questions>(questionDto);
            _questionsRepository.Update(question);
        }

        public void DeleteQuestion(int id)
        {
            _questionsRepository.DeleteById(id);
        }
    }
}
