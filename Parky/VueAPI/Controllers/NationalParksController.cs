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
    [Route("[controller]")]
    [ApiController]
    public class NationalParksController : Controller
    {
        private INationalParkReposity _npRepo;
        private readonly IMapper _mapper;

        public NationalParksController(INationalParkReposity npRepo, IMapper mapper)
        {
            _npRepo = npRepo;
            _mapper = mapper;
        }

        [HttpGet]
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

        [HttpGet("{nationalParkId:int}")]
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
        public IActionResult CreateNationalPark([FromBody]NationalParkDto nationalParkDto )
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
            return Ok(nationalParkObject);
        }
    }
}
