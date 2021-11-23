using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using VueAPI.Repository.IRepository;

namespace VueAPI.Controllers
{
    [Route("api/[controller]")]
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
    }
}
