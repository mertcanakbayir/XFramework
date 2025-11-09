using AutoMapper;
using MyApp.DAL.Entities;
using MyApp.Dtos;
using MyApp.Dtos.Role;

namespace MyApp.BLL.Mappings
{
    public class RoleProfile : Profile
    {
        public RoleProfile()
        {
            CreateMap<Role, RoleDto>().ReverseMap();

            CreateMap<RoleAddDto, Role>().ReverseMap();
        }
    }
}
