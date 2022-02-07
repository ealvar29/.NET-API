using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using VueAPI.Models;
using VueAPI.Models.Dtos;
using VueAPI.Repository.IRepository;

namespace VueAPI.Controllers
{
    [Route("api/v{version:apiVersion}/nationalparks")]
    [ApiController]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public class NationalParksController : Controller
    {
        private INationalParkRepository _npRepo;
        private readonly IMapper _mapper;

        public NationalParksController(INationalParkRepository npRepo, IMapper mapper)
        {
            _npRepo = npRepo;
            _mapper = mapper;
        }

        /// <summary>
        /// Get list of national parks.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(200, Type = typeof(List<NationalParkDto>))]
        public IActionResult GetNationalParks()
        {
            var nationalParks = _npRepo.GetNationalParks();
            var nationalParksDto = new List<NationalParkDto>();
            foreach (var park in nationalParksDto)
            {
                nationalParksDto.Add(_mapper.Map<NationalParkDto>(park));
            }

            return Ok(nationalParks);
        }

        /// <summary>
        /// Get individual nation park
        /// </summary>
        /// <param name="nationaParkId">The Id of the national park</param>
        /// <returns></returns>
        [HttpGet("{nationalParkId:int}", Name = "GetNationalPark")]
        [ProducesResponseType(200, Type = typeof(NationalParkDto))]
        [ProducesResponseType(404)]
        [ProducesDefaultResponseType]
        public IActionResult GetNationalPark(int nationaParkId)
        {
            var park = _npRepo.GetNationalPark(nationaParkId);
            if (park == null)
            {
                return NotFound();
            }
            var parkDto = _mapper.Map<NationalParkDto>(park);
            return Ok(parkDto);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(NationalParkDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult CreateNationalPark([FromBody] NationalParkDto nationalParkDto)
        {
            if (nationalParkDto == null)
            {
                return BadRequest(ModelState);
            }
            if (_npRepo.NationalParkExists(nationalParkDto.Name))
            {
                ModelState.AddModelError("", "National Park exists!");
                return StatusCode(404, ModelState);
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var nationalParkObject = _mapper.Map<NationalPark>(nationalParkDto);
            if (!_npRepo.CreateNationalPark(nationalParkObject))
            {
                ModelState.AddModelError("", $"Something went wrong when saving the record {nationalParkObject.Name}");
                return StatusCode(500, ModelState);
            }
            return CreatedAtRoute("GetNationalPark", new { nationalParkId = nationalParkObject.Id }, nationalParkObject);
        }

        [HttpPatch("{nationalParkId:int}", Name = "UpdateNationalPark")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult UpdateNationalPark(int nationalParkId, [FromBody] NationalParkDto nationalParkDto)
        {
            if (nationalParkDto == null || nationalParkId != nationalParkDto.Id)
            {
                return BadRequest(ModelState);
            }

            var nationalParkObject = _mapper.Map<NationalPark>(nationalParkDto);
            if (!_npRepo.UpdateNationalPark(nationalParkObject))
            {
                ModelState.AddModelError("", $"Something went wrong when updating the record {nationalParkObject.Name}");
                return StatusCode(500, ModelState);
            }
            return NoContent();
        }

        [HttpDelete("{nationalParkId:int}", Name = "DeleteNationalPark")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult DeleteNationalPark(int nationalParkId, [FromBody] NationalParkDto nationalParkDto)
        {
            if (!_npRepo.NationalParkExists(nationalParkId))
            {
                return NotFound();
            }

            var nationalParkObject = _npRepo.GetNationalPark(nationalParkId);
            if (!_npRepo.DeleteNationalPark(nationalParkObject))
            {
                ModelState.AddModelError("", $"Something went wrong when removing the record {nationalParkObject.Name}");
                return StatusCode(500, ModelState);
            }
            return NoContent();
        }
    }
}
