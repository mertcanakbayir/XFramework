
using AutoMapper;
using MyApp.DAL.Entities;
using MyApp.Dtos.EndpointRole;

namespace MyApp.BLL.Mappings
{

    public class EndpointRoleProfile : Profile
    {
        public EndpointRoleProfile()
        {
            CreateMap<EndpointRole, EndpointRoleDto>().ReverseMap();
            CreateMap<EndpointRole, EndpointRoleAddDto>().ReverseMap();
            CreateMap<EndpointRole, EndpointRoleUpdateDto>().ReverseMap();
        }
    }
}
