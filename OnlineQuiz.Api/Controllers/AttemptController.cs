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
                
                return Ok(_attemptManager.StartQuizAttempt(attemptDto));
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
                _attemptManager.SubmitQuizAttempt(attemptId, submittedAnswers);
                return Ok(_attemptManager.GetResults(attemptId));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
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

        [HttpGet("GetAllAttempts")]
        public IActionResult GetAllAttempts()
        {
            return Ok(_attemptManager.GetAll());
        }

        
        
        [HttpDelete("DeleteAttempt/{id}")]
        public IActionResult DeleteAttempt(int id)
        {
            try
            {
                _attemptManager.DeleteById(id);
                return Ok("Done..");
            }catch (Exception ex) { throw new Exception(ex.Message); }
        }
    }
}
