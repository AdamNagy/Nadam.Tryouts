using AutoMapper;
using POC_DotNET_6.Models;
using POC_DotNET_6.Models.Dto;
using POC_DotNET_6.Models.Dtos;

namespace POC_DotNET_6.Mapper
{
    public class ParkyMapper : Profile
    {
        public ParkyMapper()
        {
            CreateMap<NationalPark, NationalParkDto>().ReverseMap();
            CreateMap<Trail, TrailDto>().ReverseMap();
            //CreateMap<Trail, TrailCreateDto>().ReverseMap();
            //CreateMap<Trail, TrailUpdateDto>().ReverseMap();
        }
    }
}
