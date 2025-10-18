
using AutoMapper;
using XFramework.DAL.Entities;
using XFramework.Dtos.Deneme;

namespace XFramework.BLL.Mappings
{

public class DenemeProfile : Profile
{
     public DenemeProfile()
    {
CreateMap<Deneme, DenemeDto>().ReverseMap();
CreateMap<Deneme, DenemeAddDto>().ReverseMap();
CreateMap<Deneme, DenemeUpdateDto>().ReverseMap();
    }
}
}
