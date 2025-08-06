using Dtos;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XFM.BLL.Result;
using XFM.BLL.Services.UserService;
using XFM.BLL.Utilities.Hashing;
using XFM.BLL.Utilities.JWT;
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
        private readonly IValidator<RegisterDto> _registerDtoValidator;
        private readonly IValidator<LoginDto> _loginDtoValidator;
        private readonly ITokenHelper _tokenHelper;

        public AuthService(IBaseRepository<User> baseRepository, IHashingHelper hashService, IUserService userService,IValidator<LoginDto> loginDtoValidator,IValidator<RegisterDto> registerDtoValidator, ITokenHelper tokenHelper)
        {
            _hashService = hashService;
            _baseRepository = baseRepository;
            _userService = userService;
            _loginDtoValidator = loginDtoValidator;
            _registerDtoValidator = registerDtoValidator;
            _tokenHelper = tokenHelper;
        }

        public async Task<Result<string>> Login(LoginDto loginDto)
        {
            var validationResult=_loginDtoValidator.Validate(loginDto);
            if (!validationResult.IsValid) {
                var errorMessages = validationResult.Errors.Select(e => e.ErrorMessage).ToList();
                return Result<string>.Failure("Lütfen girdiğiniz bilgileri kontrol edin.",errorMessages,400);
            }
            var existedUser = await _baseRepository.GetAsync(e => e.Email == loginDto.Email);
            if (existedUser == null)
            {
                return Result<string>.Failure("Lütfen girdiğiniz bilgileri kontrol edin.", null, 401);  
            }
            bool verifyPassword = _hashService.VerifyPassword(loginDto.Password , existedUser.Password);

            if (!verifyPassword)
                return Result<string>.Failure("Lütfen girdiğiniz bilgileri kontrol edin.", null, 401);

            CreateTokenDto createTokenDto = new CreateTokenDto
            {
                Email = existedUser.Email,
                Username = existedUser.Username,
                Id=existedUser.Id
            };
            var token = _tokenHelper.CreateToken(createTokenDto);
            if (token != null)
            {
                return Result<string>.Success(token.Token, message: "Giriş Başarılı", 200);
            }
            return Result<string>.Failure("Hata oluştu.", null, 401);
        }

        public async Task<Result<string>> Register(RegisterDto registerDto)
        {
            var validationResult=_registerDtoValidator.Validate(registerDto);
            if (!validationResult.IsValid)
            {
                var errorMessages = validationResult.Errors.Select(e => e.ErrorMessage).ToList();
                return Result<string>.Failure("Lütfen girdiğiniz bilgileri kontrol edin.", errorMessages, 200);
            }
            var existingUser=await _baseRepository.GetAsync(e=>e.Email.Trim().ToLower()== registerDto.Email.Trim().ToLower(),asNoTracking:true);
            if (existingUser != null)
            {
                return Result<string>.Failure("Lütfen benzersiz bir e-posta adresi girin.");
            }

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
