
using AutoMapper;
using XFramework.DAL.Entities;
using XFramework.Dtos.EndpointRole;

namespace XFramework.BLL.Mappings
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
