using AutoMapper;
using Dtos;
using XFramework.DAL.Entities;
using XFramework.Dtos;


    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<User, UserDto>().ReverseMap();
            CreateMap<User, UserAddDto>().ReverseMap();
            CreateMap<User, UserUpdateDto>().ReverseMap();

            CreateMap<User, RegisterDto>().ReverseMap();
        }
    }
}
