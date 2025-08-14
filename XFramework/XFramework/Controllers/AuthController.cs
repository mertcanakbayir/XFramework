using Dtos;
using Microsoft.AspNetCore.Mvc;
using XFM.BLL.Result;
using XFM.BLL.Services.AuthService;

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
    }
}
