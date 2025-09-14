
using AutoMapper;
using XFramework.DAL.Entities;
using XFramework.Dtos.User;

namespace XFramework.BLL.Mappings
{

public class UserProfile : Profile
{
     public UserProfile()
    {
CreateMap<User, UserDto>().ReverseMap();
CreateMap<User, UserAddDto>().ReverseMap();
CreateMap<User, UserUpdateDto>().ReverseMap();
    }
}
}
