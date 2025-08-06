using AutoMapper;
using Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XFM.BLL.Result;
using XFM.DAL.Abstract;
using XFM.DAL.Entities;

namespace XFM.BLL.Services.UserService
{
    public class UserService : IUserService
    {
        private readonly IMapper _mapper;
        private readonly IBaseRepository<User> _baseRepository;
        public UserService(IBaseRepository<User> baseRepository, IMapper mapper)
        {
            _baseRepository = baseRepository;
            _mapper = mapper;
        }
        public async Task<Result<UserDto>> GetUserByEmail(string email)
        {
            var user = await _baseRepository.GetAsync(x => x.Email == email);
            if (user == null)
            {
                return Result<UserDto>.Failure("Kullanıcı bulunamadı", null,404);
            }
            var userDto = _mapper.Map<UserDto>(user);
            return Result<UserDto>.Success(userDto, "kullanıcı döndü", 200);
        }

        public async Task<Result<UserDto>> GetUserById(int id)
        {
           var user = await _baseRepository.GetAsync(e=>e.Id == id);
            if(user == null)
            {
                return Result<UserDto>.Failure("Kullanıcı bulunamadı",null,404);
            }
            var userDto = _mapper.Map<UserDto>(user);
            return Result<UserDto>.Success(userDto, "Başarılı", 200);
        }
    }
}
