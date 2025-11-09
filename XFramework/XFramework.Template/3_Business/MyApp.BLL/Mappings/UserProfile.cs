
using AutoMapper;
using Dtos;
using MyApp.DAL.Entities;
using MyApp.Dtos.User;

namespace MyApp.BLL.Mappings
{

    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<User, UserDto>().ReverseMap();
            CreateMap<User, UserAddDto>().ReverseMap();
            CreateMap<User, UserUpdateDto>().ReverseMap();
            CreateMap<RegisterDto, User>().ReverseMap();

        }
    }
}
