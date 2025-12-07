using AutoMapper;
using XFramework.DAL.Entities;
using XFramework.Dtos;

namespace XFramework.BLL.Mappings
{
    public class UserRoleProfile : Profile
    {
        public UserRoleProfile()
        {
            CreateMap<User, UserRoleDto>()
                .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.RoleIds, opt => opt.MapFrom(src =>
                    src.UserRoles.Select(ur => ur.RoleId).ToList()))
                .ForMember(dest => dest.Roles, opt => opt.MapFrom(src =>
                    src.UserRoles.Select(ur => ur.Role.Name).ToList()));
        }
    }
}
