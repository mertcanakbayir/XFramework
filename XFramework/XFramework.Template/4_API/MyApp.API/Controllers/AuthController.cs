using Dtos;
using Microsoft.AspNetCore.Mvc;
using MyApp.BLL.Services.Concretes;
using MyApp.Dtos;
using MyApp.Helper.ViewModels;

namespace MyApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly AuthService _authService;
        public AuthController(AuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("register")]
        [TypeFilter(typeof(ValidateFilter))]
        public async Task<ResultViewModel<string>> Register(RegisterDto registerDto)
        {
            return await _authService.Register(registerDto);
        }
        [HttpPost("login")]
        [TypeFilter(typeof(ValidateFilter))]
        public async Task<ResultViewModel<LoginResponseDto>> Login(LoginDto loginDto)
        {
            return await _authService.Login(loginDto);
        }
        [HttpPost("forgot-password")]
        public async Task<ResultViewModel<PasswordResetTokenDto>> ForgotPassword(ForgotPasswordDto forgotPasswordDto)
        {
            return await _authService.ForgotPasswordAsync(forgotPasswordDto);
        }

        [HttpPost("reset-password")]
        public async Task<ResultViewModel<string>> ResetPassword(ResetPasswordDto resetPasswordDto)
        {
            return await _authService.ResetPasswordAsync(resetPasswordDto);
        }
    }
}
