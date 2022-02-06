using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using VueAPI.Models;
using VueAPI.Models.Dtos;
using VueAPI.Repository.IRepository;

namespace VueAPI.Controllers
{
    //[Route("api/[controller]")]
    [Route("api/Trails")]
    [ApiController]
    [ApiExplorerSettings(GroupName = "ParkyOpenAPISpecTrails")]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public class TrailsController : Controller
    {
        private ITrailRepository _trailRepo;
        private readonly IMapper _mapper;

        public TrailsController(ITrailRepository trailRepo, IMapper mapper)
        {
            _trailRepo = trailRepo;
            _mapper = mapper;
        }

        /// <summary>
        /// Get list of trails.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(200, Type = typeof(List<TrailDto>))]
        public IActionResult GetTrails()
        {
            var trails = _trailRepo.GetTrails();
            var trailDto = new List<TrailDto>();
            foreach (var trail in trailDto)
            {
                trailDto.Add(_mapper.Map<TrailDto>(trail));
            }

            return Ok(trails);
        }

        /// <summary>
        /// Get individual trail
        /// </summary>
        /// <param name="trailId">The Id of the national park</param>
        /// <returns></returns>
        [HttpGet("{trailId:int}", Name = "GetTrail")]
        [ProducesResponseType(200, Type = typeof(TrailDto))]
        [ProducesResponseType(404)]
        [ProducesDefaultResponseType]
        public IActionResult GetNationalPark(int trailId)
        {
            var trail = _trailRepo.GetTrail(trailId);
            if (trail == null)
            {
                return NotFound();
            }
            var trailDto = _mapper.Map<TrailDto>(trail);
            return Ok(trailDto);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(TrailDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult CreateTrail([FromBody] TrailCreateDto trailDto)
        {
            if (trailDto == null)
            {
                return BadRequest(ModelState);
            }
            if (_trailRepo.TrailExists(trailDto.Name))
            {
                ModelState.AddModelError("", "Trail exists!");
                return StatusCode(404, ModelState);
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var trailObject = _mapper.Map<Trail>(trailDto);
            if (!_trailRepo.CreateTrail(trailObject))
            {
                ModelState.AddModelError("", $"Something went wrong when saving the record {trailObject.Name}");
                return StatusCode(500, ModelState);
            }
            return CreatedAtRoute("GetTrail", new { trailId = trailObject.Id }, trailObject);
        }

        [HttpPatch("{trailId:int}", Name = "UpdateTrail")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult UpdateTrail(int trailId, [FromBody] TrailUpdateDto trailDto)
        {
            if (trailDto == null || trailId != trailDto.Id)
            {
                return BadRequest(ModelState);
            }

            var trailObject = _mapper.Map<Trail>(trailDto);
            if (!_trailRepo.UpdateTrail(trailObject))
            {
                ModelState.AddModelError("", $"Something went wrong when updating the record {trailObject.Name}");
                return StatusCode(500, ModelState);
            }
            return NoContent();
        }

        [HttpDelete("{trailId:int}", Name = "DeleteTrail")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult DeleteTrail(int trailId, [FromBody] TrailDto trailDto)
        {
            if (!_trailRepo.TrailExists(trailId))
            {
                return NotFound();
            }

            var trailObject = _trailRepo.GetTrail(trailId);
            if (!_trailRepo.DeleteTrail(trailObject))
            {
                ModelState.AddModelError("", $"Something went wrong when removing the record {trailObject.Name}");
                return StatusCode(500, ModelState);
            }
            return NoContent();
        }
    }
}
