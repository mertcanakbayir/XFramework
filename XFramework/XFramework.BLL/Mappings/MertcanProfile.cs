
using AutoMapper;
using XFramework.DAL.Entities;
using XFramework.Dtos.Mertcan;

namespace XFramework.BLL.Mappings
{

public class MertcanProfile : Profile
{
     public MertcanProfile()
    {
CreateMap<Mertcan, MertcanDto>().ReverseMap();
CreateMap<Mertcan, MertcanAddDto>().ReverseMap();
CreateMap<Mertcan, MertcanUpdateDto>().ReverseMap();
    }
}
}
