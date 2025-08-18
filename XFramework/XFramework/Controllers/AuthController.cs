using Dtos;
using Microsoft.AspNetCore.Mvc;
using XFM.BLL.Result;
using XFM.BLL.Services.AuthService;
using XFramework.Dtos;

namespace XFramework.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("register")]
        [ValidateFilter]
        public async Task<ResultViewModel<string>> Register(RegisterDto registerDto)
        {
            return await _authService.Register(registerDto);
        }
        [HttpPost("login")]
        [ValidateFilter]
        public async Task<ResultViewModel<string>> Login(LoginDto loginDto)
        {
            return await _authService.Login(loginDto);
        }
        // TODO: forgot-password'a ve resetPasword'a fluentValidation eklenecek
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
