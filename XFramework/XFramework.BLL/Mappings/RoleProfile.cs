using AutoMapper;
using XFramework.DAL.Entities;
using XFramework.Dtos;
using XFramework.Dtos.Role;

namespace XFramework.BLL.Mappings
{
    public class RoleProfile : Profile
    {
        public RoleProfile()
        {
            CreateMap<Role, RoleDto>();

            CreateMap<RoleAddDto, Role>();
        }
    }
}
