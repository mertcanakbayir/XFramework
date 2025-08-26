using AutoMapper;
using Dtos;
using XFramework.DAL.Entities;
using XFramework.Dtos;


namespace XFM.BLL.Mappings
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {

            CreateMap<User, UserDto>();

            CreateMap<RegisterDto, User>()
             .ForMember(dest => dest.Password, opt => opt.Ignore())
             .ForMember(dest => dest.CreatedAt, opt => opt.Ignore())
             .ForMember(dest => dest.UpdatedAt, opt => opt.Ignore())
             .ForMember(dest => dest.IsActive, opt => opt.Ignore());

            CreateMap<UserAddDto, User>();

            CreateMap<UserUpdateDto, User>();
        }
    }
}
