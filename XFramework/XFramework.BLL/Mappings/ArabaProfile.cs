
using AutoMapper;
using XFramework.DAL.Entities;
using XFramework.Dtos.Araba;

namespace XFramework.BLL.Mappings
{

public class ArabaProfile : Profile
{
     public ArabaProfile()
    {
CreateMap<Araba, ArabaDto>().ReverseMap();
CreateMap<Araba, ArabaAddDto>().ReverseMap();
CreateMap<Araba, ArabaUpdateDto>().ReverseMap();
    }
}
}
