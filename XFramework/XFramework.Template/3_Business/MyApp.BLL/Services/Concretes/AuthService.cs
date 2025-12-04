using AutoMapper;
using Dtos;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using MyApp.BLL.Utilities.JWT;
using MyApp.BLL.Services.Abstracts;
using MyApp.BLL.Utilities.Hashing;
using MyApp.DAL.Entities;
using MyApp.Dtos;
using MyApp.Dtos.User;
using MyApp.Helper.ViewModels;
using MyApp.Repository.Repositories.Abstract;

namespace MyApp.BLL.Services.Concretes
{
    public class AuthService : IRegister
    {

        private readonly IHashingHelper _hashService;
        private readonly IBaseRepository<User> _userRepository;
        private readonly IValidator<RegisterDto> _registerDtoValidator;
        private readonly IValidator<LoginDto> _loginDtoValidator;
        private readonly IValidator<ForgotPasswordDto> _forgotPasswordDtoValidator;
        private readonly IValidator<ResetPasswordDto> _resetPasswordDtoValidator;
        private readonly ITokenHelper _tokenHelper;
        private readonly IMapper _mapper;
        private readonly MailService _mailService;
        private readonly IBaseRepository<UserRole> _userRoleRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly RoleAuthorizationService _roleAuthorizationService;

        public AuthService(IBaseRepository<User> userRepository, IBaseRepository<UserRole> userRoleRespository, IHashingHelper hashService, IValidator<LoginDto> loginDtoValidator, IValidator<RegisterDto> registerDtoValidator, ITokenHelper tokenHelper,
            IMapper mapper, MailService mailService, IValidator<ForgotPasswordDto> forgotPasswordDtoValidator, IValidator<ResetPasswordDto> resetPasswordDtoValidator, IUnitOfWork unitOfWork, RoleAuthorizationService roleAuthorizationService)
        {
            _hashService = hashService;
            _userRepository = userRepository;
            _userRoleRepository = userRoleRespository;
            _loginDtoValidator = loginDtoValidator;
            _registerDtoValidator = registerDtoValidator;
            _tokenHelper = tokenHelper;
            _mapper = mapper;
            _mailService = mailService;
            _forgotPasswordDtoValidator = forgotPasswordDtoValidator;
            _resetPasswordDtoValidator = resetPasswordDtoValidator;
            _unitOfWork = unitOfWork;
            _roleAuthorizationService = roleAuthorizationService;
        }

        public async Task<ResultViewModel<LoginResponseDto>> Login(LoginDto loginDto)
        {
            var validationResult = _loginDtoValidator.Validate(loginDto);
            if (!validationResult.IsValid)
            {
                var errorMessages = validationResult.Errors.Select(e => e.ErrorMessage).ToList();
                return ResultViewModel<LoginResponseDto>.Failure("Please check credentials.", errorMessages, 400);
            }
            var existedUser = await _userRepository.GetAsync(filter: e => e.Email == loginDto.Email, include: query => query.Include(u => u.UserRoles).ThenInclude(ur => ur.Role));
            if (existedUser == null)
            {
                return ResultViewModel<LoginResponseDto>.Failure("Please check credentials.", null, 401);
            }
            var roles = existedUser.UserRoles.Select(ur => ur.Role.Name).ToList();
            bool verifyPassword = _hashService.VerifyPassword(loginDto.Password, existedUser.Password);
            if (!verifyPassword)
                return ResultViewModel<LoginResponseDto>.Failure("Please check credentials.", null, 401);
            var userPages = await _roleAuthorizationService.GetAllPagesByUser(existedUser.Id);
            if (userPages == null)
            {
                return ResultViewModel<LoginResponseDto>.Failure("User page information not found.", null, 401);
            }
            var createTokenDto = _mapper.Map<CreateTokenDto>(existedUser);
            var token = _tokenHelper.CreateToken(createTokenDto);
            var data = new LoginResponseDto
            {
                Token = token.Token,
                UserPages = userPages
            };
            if (token != null)
            {
                return ResultViewModel<LoginResponseDto>.Success(data, message: "Login succesful.", 200);
            }
            return ResultViewModel<LoginResponseDto>.Failure("An eror accoured.", null, 401);
        }

        public async Task<ResultViewModel<string>> Register(RegisterDto registerDto)
        {
            var validationResult = _registerDtoValidator.Validate(registerDto);
            if (!validationResult.IsValid)
            {
                var errorMessages = validationResult.Errors.Select(e => e.ErrorMessage).ToList();
                return ResultViewModel<string>.Failure("Please check credentials.", errorMessages, 200);
            }
            var existingUser = await _userRepository.GetAsync(filter: e => e.Email.Trim().ToLower() == registerDto.Email.Trim().ToLower(),
                asNoTracking: true);
            if (existingUser != null)
            {
                return ResultViewModel<string>.Failure("Please provide a unique email address.");
            }

            var hashPassword = _hashService.HashPassword(registerDto.Password);
            var user = _mapper.Map<User>(registerDto);
            user.Password = _hashService.HashPassword(registerDto.Password);
            user.IsActive = true;
            user.CreatedAt = DateTime.Now;
            user.UpdatedAt = DateTime.Now;

            await _unitOfWork.BeginTransactionAsync();

            var userBaseRepo = _unitOfWork.GetRepository<User>();
            await userBaseRepo.AddAsync(user);
            await _unitOfWork.SaveChangesAsync();
            var userRole = new UserRole
            {
                RoleId = 3,
                UserId = user.Id
            };
            var userRoleBaseRepo = _unitOfWork.GetRepository<UserRole>();
            await userRoleBaseRepo.AddAsync(userRole);
            await _unitOfWork.CommitTransactionAsync();
            return ResultViewModel<string>.Success("User registered succesfully.", 201);
        }

        public async Task<ResultViewModel<string>> ResetPasswordAsync(ResetPasswordDto resetPasswordDto)
        {
            var resetPasswordDtoValidation = _resetPasswordDtoValidator.Validate(resetPasswordDto);
            if (!resetPasswordDtoValidation.IsValid)
            {
                var errorMessages = resetPasswordDtoValidation.Errors.Select(e => e.ErrorMessage).ToList();
                return ResultViewModel<string>.Failure("Please check credentials.", errorMessages, 400);
            }
            var user = await _userRepository.GetAsync(filter: u => u.Email == resetPasswordDto.Email);
            if (user == null)
            {
                return ResultViewModel<string>.Failure("User not found.", null, 400);
            }

            var userDto = _mapper.Map<UserDto>(user);

            if (!_tokenHelper.ValidatePasswordResetToken(resetPasswordDto.Token, userDto))
            {
                return ResultViewModel<string>.Failure("Token is invalid or expired.", null, 401);
            }

            var hashedPassword = _hashService.HashPassword(resetPasswordDto.NewPassword);

            user.Password = hashedPassword;
            await _userRepository.UpdateAsync(user);
            await _unitOfWork.SaveChangesAsync();

            return ResultViewModel<string>.Success("Password updated succesfully.", null, 200);
        }

        public async Task<ResultViewModel<PasswordResetTokenDto>> ForgotPasswordAsync(ForgotPasswordDto forgotPasswordDto)
        {
            var validationResult = _forgotPasswordDtoValidator.Validate(forgotPasswordDto);
            if (!validationResult.IsValid)
            {
                var errorMessages = validationResult.Errors.Select(e => e.ErrorMessage).ToList();
                return ResultViewModel<PasswordResetTokenDto>.Failure("Please check credentials", errorMessages, 400);
            }
            var user = await _userRepository.GetAsync(filter: u => u.Email == forgotPasswordDto.Email);

            if (user == null)
            {
                return ResultViewModel<PasswordResetTokenDto>.Failure("User not found.", null, 400);
            }
            var userDto = _mapper.Map<UserDto>(user);
            var resetTokenDto = _tokenHelper.CreatePasswordResetToken(userDto);
            var resetLink = $"https://localhost:4200/reset-password?email={userDto.Email}&token={resetTokenDto.Token}";
            var mailResult = await _mailService.SendDirectMailAsync(
                 userDto.Email,
                 "Password Reset Request",
                 $"Your password reset link: {resetLink}",
                 settingId: 3
             );

            if (!mailResult.IsSuccess)
            {
                return ResultViewModel<PasswordResetTokenDto>.Failure("An error ocurred while sending email: " + mailResult.Message, null, 500);
            }
            return ResultViewModel<PasswordResetTokenDto>.Success(resetTokenDto, "Password reset token generated.", 200);

        }
    }
}
