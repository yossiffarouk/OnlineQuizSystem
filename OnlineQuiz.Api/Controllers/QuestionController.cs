using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OnlineQuiz.BLL.Dtos.Question;

using OnlineQuiz.BLL.Managers.QuestionManager;

namespace OnlineQuiz.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QuestionController : ControllerBase
    {
        private readonly IQuestionManager _questionManager;
       

        public QuestionController(IQuestionManager questionManager)
        {
            _questionManager = questionManager;
           
        }

        
        [HttpGet]
        public IActionResult GetAllQuestions()
        {
            var questions = _questionManager.GetAllQuestions();
            return Ok(questions); 
        }

    
        [HttpGet("{id}")]
        public IActionResult GetQuestionById(int id)
        {
            var question = _questionManager.GetQuestionById(id);
            if (question == null)
            {
                return NotFound();
            }
            return Ok(question);
        }

   
        [HttpPost]
        public IActionResult AddQuestion([FromBody] createQuestionDto createquestionDto)
        {
            if (createquestionDto == null)
            {
                return BadRequest();
            }

            _questionManager.AddQuestion(createquestionDto);
            return Ok("Question added successfully.");
        }

     
        [HttpPut("{id}")]
        public IActionResult UpdateQuestion(int id, [FromBody] QuestionDto questionDto)
        {
            if (id != questionDto.Id || questionDto == null)
            {
                return BadRequest("Invalid question data.");
            }

            try
            {
                _questionManager.UpdateQuestion(questionDto);
                return Ok("Question updated successfully.");
            }
            catch (InvalidOperationException)
            {
                return NotFound("Question not found.");
            }
        }

 
        [HttpDelete("{id}")]
        public IActionResult DeleteQuestion(int id)
        {
            var existingQuestion = _questionManager.GetQuestionById(id);
            if (existingQuestion == null)
            {
                return NotFound();
            }

            _questionManager.DeleteQuestion(id);
            return Ok("Question deleted successfully.");
        }

    }
}
