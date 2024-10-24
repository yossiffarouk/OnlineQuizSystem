using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using OnlineQuiz.BLL.Dtos.Answer;
using OnlineQuiz.BLL.Dtos.Attempt;
using OnlineQuiz.BLL.Managers.Answer;
using OnlineQuiz.BLL.Managers.Attempt;
using OnlineQuiz.BLL.Managers.Base;
using OnlineQuiz.BLL.Managers.Track;
using OnlineQuiz.DAL.Data.DBHelper;

namespace OnlineQuiz.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AnswersController : ControllerBase
    {
        private readonly IAnswersManager _answerManager;
        public AnswersController(IAnswersManager answerManager)
        {
            _answerManager = answerManager;
        }

        [HttpPost("submit")]
        public IActionResult SubmitAnswer(AnswerDto request)
        {

            _answerManager.SubmitAnswer(request.AttemptId, request.QuestionId, request.SubmittedAnswer);
            return Ok(new { message = "Answer submitted successfully." });
        }

        [HttpGet("{attemptId}")]
        public IActionResult GetUserAnswers(int attemptId)
        {
            var answers = _answerManager.GetUserAnswers(attemptId);
            if (answers != null)
            {
                return Ok(answers);
            }

            return NotFound("There is no answers for that AttemptId");
        }

        [HttpGet("CorrectAnswers/{quizId}")]
        public IActionResult GetCorrectAnswers(int quizId)
        {
            var correctAnswers = _answerManager.GetCorrectAnswers(quizId);
            return Ok(correctAnswers);
        }
        [HttpGet("GetAnswer/{answerId}")]
        public IActionResult GetById(int answerId)
        {

            var result = _answerManager.AnswerExist(answerId);

            if (result != null)
            {
                return Ok(result);
            }

            return NotFound("There is no answer with that id");

        }
    }


}

