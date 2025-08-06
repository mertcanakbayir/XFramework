using Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XFM.BLL.HashService;
using XFM.BLL.Result;
using XFM.BLL.Services.UserService;
using XFM.DAL.Abstract;
using XFM.DAL.Concrete;
using XFM.DAL.Entities;

namespace XFM.BLL.Services.AuthService
{
    public class AuthService : IAuthService
    {

        private readonly IHashingHelper _hashService;
        private readonly IBaseRepository<User> _baseRepository;
        private readonly IUserService _userService;

        public AuthService(IBaseRepository<User> baseRepository, IHashingHelper hashService, IUserService userService)
        {
            _hashService = hashService;
            _baseRepository = baseRepository;
            _userService = userService;
        }

        public async Task<Result<string>> Login(LoginDto loginDto)
        {
            var existedUser = await _userService.GetUserByEmail(loginDto.Mail);
            
            bool verifyPassword = _hashService.VerifyPassword(loginDto.Password , existedUser.Data.Password);

            if (!verifyPassword)
                return Result<string>.Failure("Geçersiz şifre", null, 401);

            return Result<string>.Success(null,message:"Giriş Başarılı", 200);

        }

        public async Task<Result<string>> Register(RegisterDto registerDto)
        {
            
           var hashPassword = _hashService.HashPassword(registerDto.Password);
            var user = new User
            {
                Username = registerDto.Username,
                Password = hashPassword,
                Email = registerDto.Email,
                IsActive=true,
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now
            };
             await _baseRepository.AddAsync(user);

            return Result<string>.Success("Kullanıcı Başarıyla Kaydedildi.",201);
            
        }
    }
}
