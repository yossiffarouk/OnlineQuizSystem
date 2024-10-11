using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OnlineQuiz.BLL.Dtos.Track;
using OnlineQuiz.BLL.Managers.Track;
using OnlineQuiz.DAL.Data.Models;
using OnlineQuiz.DAL.Repositoryies.TrackRepository;

namespace OnlineQuiz.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TrackController : ControllerBase
    {
        private readonly ITrackManager _trackManager;

        public TrackController(ITrackManager trackManager)
        {
            _trackManager = trackManager;
        }

        // GET: api/track
        [HttpGet]
        public ActionResult<IEnumerable<TrackDto>> GetAll()
        {
            var tracks = _trackManager.GetAll().ToList();
            return Ok(tracks);
        }

        // GET: api/track/{id}
        [HttpGet("{id}")]
        public ActionResult<TrackDto> GetById(int id)
        {
            var track = _trackManager.GetById(id);
            if (track == null)
            {
                return NotFound();
            }
            return Ok(track);
        }

        // POST: api/track
        [HttpPost]
        public ActionResult<CreateTrackDTO> Add([FromBody] CreateTrackDTO trackDto)
        {
            if (trackDto == null)
            {
                return BadRequest();
            }

            _trackManager.Add(trackDto);
            return Ok(trackDto);
        }

        // PUT: api/track/{id}
        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] TrackDto trackDto)
        {
            if (trackDto == null || id != trackDto.Id)
            {
                return BadRequest();
            }

            _trackManager.Update(trackDto);
            return NoContent();
        }

        // DELETE: api/track/{id}
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _trackManager.DeleteById(id);
            return NoContent();
        }
    }
}
