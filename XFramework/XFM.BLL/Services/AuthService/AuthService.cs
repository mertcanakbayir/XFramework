using AutoMapper;
using Dtos;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using XFM.BLL.Result;
using XFM.BLL.Services.UserService;
using XFM.BLL.Utilities.Hashing;
using XFM.BLL.Utilities.JWT;
using XFM.DAL.Abstract;
using XFM.DAL.Entities;
using XFramework.BLL.Services.MailService;
using XFramework.DAL.Entities;
using XFramework.Dtos;

namespace XFM.BLL.Services.AuthService
{
    public class AuthService : IAuthService
    {

        private readonly IHashingHelper _hashService;
        private readonly IBaseRepository<User> _userRepository;
        private readonly IBaseRepository<UserRole> _userRoleRepository;
        private readonly IUserService _userService;
        private readonly IValidator<RegisterDto> _registerDtoValidator;
        private readonly IValidator<LoginDto> _loginDtoValidator;
        private readonly ITokenHelper _tokenHelper;
        private readonly IMapper _mapper;
        private readonly MailService _mailService;

        public AuthService(IBaseRepository<User> userRepository, IBaseRepository<UserRole> userRoleRespository, IHashingHelper hashService, IUserService userService, IValidator<LoginDto> loginDtoValidator, IValidator<RegisterDto> registerDtoValidator, ITokenHelper tokenHelper,
            IMapper mapper, MailService mailService)
        {
            _hashService = hashService;
            _userRepository = userRepository;
            _userRoleRepository = userRoleRespository;
            _userService = userService;
            _loginDtoValidator = loginDtoValidator;
            _registerDtoValidator = registerDtoValidator;
            _tokenHelper = tokenHelper;
            _mapper = mapper;
            _mailService = mailService;
        }

        public async Task<ResultViewModel<string>> Login(LoginDto loginDto)
        {
            var validationResult = _loginDtoValidator.Validate(loginDto);
            if (!validationResult.IsValid)
            {
                var errorMessages = validationResult.Errors.Select(e => e.ErrorMessage).ToList();
                return ResultViewModel<string>.Failure("Lütfen girdiğiniz bilgileri kontrol edin.", errorMessages, 400);
            }
            var existedUser = await _userRepository.GetAsync(e => e.Email == loginDto.Email, includeFunc: query => query.Include(u => u.UserRoles).ThenInclude(ur => ur.Role));

            if (existedUser == null)
            {
                return ResultViewModel<string>.Failure("Lütfen girdiğiniz bilgileri kontrol edin.", null, 401);
            }
            var roles = existedUser.UserRoles.Select(ur => ur.Role.Name).ToList();
            bool verifyPassword = _hashService.VerifyPassword(loginDto.Password, existedUser.Password);

            if (!verifyPassword)
                return ResultViewModel<string>.Failure("Lütfen girdiğiniz bilgileri kontrol edin.", null, 401);

            var createTokenDto = _mapper.Map<CreateTokenDto>(existedUser);
            var token = _tokenHelper.CreateToken(createTokenDto);
            if (token != null)
            {
                return ResultViewModel<string>.Success(token.Token, message: "Giriş Başarılı", 200);
            }
            return ResultViewModel<string>.Failure("Hata oluştu.", null, 401);
        }

        public async Task<ResultViewModel<string>> Register(RegisterDto registerDto)
        {
            var validationResult = _registerDtoValidator.Validate(registerDto);
            if (!validationResult.IsValid)
            {
                var errorMessages = validationResult.Errors.Select(e => e.ErrorMessage).ToList();
                return ResultViewModel<string>.Failure("Lütfen girdiğiniz bilgileri kontrol edin.", errorMessages, 200);
            }
            var existingUser = await _userRepository.GetAsync(e => e.Email.Trim().ToLower() == registerDto.Email.Trim().ToLower(), asNoTracking: true);
            if (existingUser != null)
            {
                return ResultViewModel<string>.Failure("Lütfen benzersiz bir e-posta adresi girin.");
            }

            var hashPassword = _hashService.HashPassword(registerDto.Password);
            var user = _mapper.Map<User>(registerDto);
            user.Password = _hashService.HashPassword(registerDto.Password);
            user.IsActive = true;
            user.CreatedAt = DateTime.Now;
            user.UpdatedAt = DateTime.Now;
            await _userRepository.AddAsync(user);

            var userRole = new UserRole
            {
                RoleId = 3,
                UserId = user.Id
            };
            await _userRoleRepository.AddAsync(userRole);

            return ResultViewModel<string>.Success("Kullanıcı Başarıyla Kaydedildi.", 201);

        }

        public async Task<ResultViewModel<string>> ResetPasswordAsync(ResetPasswordDto resetPasswordDto)
        {
            if (resetPasswordDto.NewPassword != resetPasswordDto.ConfirmPassword)
            {
                return ResultViewModel<string>.Failure("Şifreler uyuşmuyor", null, 400);
            }
            var user = await _userRepository.GetAsync(u => u.Email == resetPasswordDto.Email);
            if (user == null)
            {
                return ResultViewModel<string>.Failure("Kullanıcı Bulunamadı.", null, 400);
            }

            var userDto = _mapper.Map<UserDto>(user);

            if (!_tokenHelper.ValidatePasswordResetToken(resetPasswordDto.Token, userDto))
            {
                return ResultViewModel<string>.Failure("Token geçersiz veya süresi dolmuş.", null, 401);
            }

            var hashedPassword = _hashService.HashPassword(resetPasswordDto.NewPassword);

            user.Password = hashedPassword;
            await _userRepository.UpdateAsync(user);

            return ResultViewModel<string>.Success("Şifre başarıyla güncellendi.", null, 200);
        }

        public async Task<ResultViewModel<PasswordResetTokenDto>> ForgotPasswordAsync(ForgotPasswordDto forgotPasswordDto)
        {
            var user = await _userRepository.GetAsync(u => u.Email == forgotPasswordDto.Email);
            if (user == null)
            {
                return ResultViewModel<PasswordResetTokenDto>.Failure("Kullanıcı bulunamadı.", null, 400);
            }
            var userDto = _mapper.Map<UserDto>(user);
            var resetTokenDto = _tokenHelper.CreatePasswordResetToken(userDto);

            var resetLink = $"https://frontend.com/reset-password?email={userDto.Email}&token={resetTokenDto.Token}";

            var mailResult = await _mailService.SendEmailAsync(
       userDto.Email,
       "Şifre Sıfırlama Talebi",
       $"Şifre sıfırlama linkiniz: {resetLink}",
       settingId: 3,
       isQueue:true
            );

            if (!mailResult.IsSuccess)
                return ResultViewModel<PasswordResetTokenDto>.Failure("Mail gönderilirken hata oluştu: " + mailResult.Message, null, 500);


            return ResultViewModel<PasswordResetTokenDto>.Success(resetTokenDto, "Şifre sıfırlama tokeni üretildi", 200);

        }
    }
}
