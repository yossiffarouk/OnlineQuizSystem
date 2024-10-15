using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OnlineQuiz.BLL.Dtos.Attempt;
using OnlineQuiz.BLL.Managers.Attempt;
using OnlineQuiz.DAL.Data.Models;
using OnlineQuiz.DAL.Repositoryies.AttemptRepository;
namespace OnlineQuiz.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AttemptController : ControllerBase
    {
       
        private readonly IAttemptManager _attemptManager;
        private readonly IMapper _mapper;
        public AttemptController(IAttemptManager attemptManager, IMapper mapper)
        {
            _attemptManager = attemptManager;
            _mapper = mapper;
            
        }


        [HttpPost("StartQuizAttempt")]
        public IActionResult StartQuizAttempt([FromBody]StartQuizAttemptDto attemptDto)
        {
            try
            {
                _attemptManager.StartQuizAttempt(attemptDto);
                return Ok(new { message = "Quiz attempt started successfully" });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("SubmitQuizAttempt")]
        public IActionResult SubmitQuizAttempt(int attemptId, List<AnswerDto> submittedAnswers)
        {

            try
            {
                var submittedAnswer = _mapper.Map<List<Answers>>(submittedAnswers);
                _attemptManager.SubmitQuizAttempt(attemptId, submittedAnswers);
                return Ok(new { message = "Quiz attempt submitted successfully" });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("GetResults")]
        public IActionResult GetResults(int attemptId)
        {
            
            try
            {
                var result = _attemptManager.GetResults(attemptId);

                return Ok(result); 
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpGet("GetUserAttempts")]
        public IActionResult GetUserAttempts(string studentId)
        {
            try
            {
                var attempts = _attemptManager.GetUserAttempts(studentId);

                return Ok(attempts);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }
        [HttpGet("GetTotalScoreByStudent")]
        public IActionResult GetTotalScoreByStudent(string studentId)
        {
            try
            {
                var quizScores = _attemptManager.GetTotalScoresByStudentId(studentId);
                return Ok(quizScores);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
