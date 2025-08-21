using AutoMapper;
using XFramework.DAL.Entities;
using XFramework.Dtos;

namespace XFramework.BLL.Mappings
{
    public class RoleProfile : Profile
    {
        public RoleProfile()
        {
            CreateMap<Role, RoleDto>();

            CreateMap<RoleAddDto, Role>();

            CreateMap<PageRoleAddDto, PageRole>();
            CreateMap<PageRole, PageRoleAddDto>();

            CreateMap<EndpointRoleAddDto, EndpointRole>();
        }
    }
}
