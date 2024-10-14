using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OnlineQuiz.BLL.Dtos.Quiz;
using OnlineQuiz.BLL.Managers.Quiz;
using static OnlineQuiz.BLL.Dtos.Quiz.QuizDto;

namespace OnlineQuiz.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QuizController : ControllerBase
    {
        private readonly IQuizManager _quizManager;

        public QuizController(IQuizManager quizManager)
        {
            _quizManager = quizManager;
        }

        [HttpGet]
        public ActionResult<IEnumerable<QuizDto>> GetAllQuizzes()
        {
            var quizzes = _quizManager.GetAllQuizzes();
            return Ok(quizzes.ToList());
        }

        [HttpGet("{id}")]
        public ActionResult<QuizDto> GetQuizById(int id)
        {
            var quiz = _quizManager.GetQuizById(id);
            if (quiz == null)
            {
                return NotFound();
            }
            return Ok(quiz);
        }

        [HttpPost]
        public ActionResult<QuizDto> AddQuiz([FromBody] CreatQuizDTO quizDto)
        {
            if (quizDto == null)
            {
                return BadRequest("Quiz data is null.");
            }

            var createdQuiz = _quizManager.AddQuiz(quizDto);
            return Ok("Quiz added successfully.");
        }

        [HttpPut("{id}")]
        public IActionResult UpdateQuiz(int id, [FromBody] QuizDto quizDto)
        {
            if (id != quizDto.Id)
            {
                return BadRequest("Quiz ID dont match.");
            }

            _quizManager.UpdateQuiz(quizDto);
            return Ok("Quiz updated successfully.");
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteQuiz(int id)
        {
            _quizManager.DeleteQuiz(id);
            return Ok("Quiz deleted successfully.");
        }

        [HttpGet("track/{trackId}")]
        public ActionResult<IEnumerable<QuizDto>> GetQuizzesByTrackId(int trackId)
        {
            var quizzes = _quizManager.GetQuizzesByTrackId(trackId);
            return Ok(quizzes.ToList());
        }
         [HttpGet("withQuestionsAndOptions")]
        public IActionResult GetQuizzesWithQuestionsAndOptions()
        {
            var quizzes = _quizManager.GetQuizzesWithQuestionsAndOptions();
            return Ok(quizzes);
        }
        [HttpGet("withQuestionsAndOptions/{id}")]
        public ActionResult<FinalQuizDTO> GetQuizByIdWithQuestions(int id)
        {
            var quizDto = _quizManager.GetQuizByIdWithQuestions(id);
            if (quizDto == null)
            {
                return NotFound(); // Return 404 if the quiz is not found
            }
            return Ok(quizDto); // Return the quiz DTO with questions and options
        }
        [HttpGet("{id}/shuffled")]
        public IActionResult GetQuizWithShuffledQuestions(int id)
        {
            var finalQuizDto = _quizManager.GetQuizWithShuffledQuestions(id);

            if (finalQuizDto == null)
            {
                return NotFound();
            }

            return Ok(finalQuizDto);
        }
        [HttpGet("available")]
        public ActionResult<IEnumerable<QuizDto>> GetAvailableQuizzes()
        {
            var availableQuizzes = _quizManager.GetAvailableQuizzes();
            return Ok(availableQuizzes.ToList());
        }

    }
}
