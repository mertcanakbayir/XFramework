
using AutoMapper;
using XFramework.DAL.Entities;
using XFramework.Dtos.Kerem;

namespace XFramework.BLL.Mappings
{

public class KeremProfile : Profile
{
     public KeremProfile()
    {
CreateMap<Kerem, KeremDto>().ReverseMap();
CreateMap<Kerem, KeremAddDto>().ReverseMap();
CreateMap<Kerem, KeremUpdateDto>().ReverseMap();
    }
}
}
