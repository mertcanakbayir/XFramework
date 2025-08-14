using AutoMapper;
using Dtos;
using XFM.DAL.Entities;

namespace XFramework.BLL.Mappings
{
    public class TokenProfile:Profile
    {
        public TokenProfile()
        {
            CreateMap<User, CreateTokenDto>()
                .ForMember(dest => dest.Role,
                           opt => opt.MapFrom(src => src.UserRoles.Select(ur => ur.Role.Name).ToList()));
        }
    }
}
