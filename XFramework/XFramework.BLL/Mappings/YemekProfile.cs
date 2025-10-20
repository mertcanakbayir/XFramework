
using AutoMapper;
using XFramework.DAL.Entities;
using XFramework.Dtos.Yemek;

namespace XFramework.BLL.Mappings
{

public class YemekProfile : Profile
{
     public YemekProfile()
    {
CreateMap<Yemek, YemekDto>().ReverseMap();
CreateMap<Yemek, YemekAddDto>().ReverseMap();
CreateMap<Yemek, YemekUpdateDto>().ReverseMap();
    }
}
}
