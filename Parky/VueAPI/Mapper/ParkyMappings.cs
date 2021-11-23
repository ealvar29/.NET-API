using AutoMapper;
using VueAPI.Models;
using VueAPI.Models.Dtos;

namespace VueAPI.Mapper
{
    public class ParkyMappings : Profile
    {
        public ParkyMappings()
        {
            CreateMap<NationalPark, NationalParkDto>().ReverseMap();
        }
    }
}
