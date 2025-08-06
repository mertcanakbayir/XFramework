using Dtos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using XFM.BLL.Services.AuthService;
using XFM.BLL.Services.UserService;
using XFM.DAL.Entities;

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
        public async Task<ActionResult> Register(RegisterDto registerDto)
        {
            try
            {
                var result = await _authService.Register(registerDto);
                if (!result.IsSuccess)
                {
                    return StatusCode(result.StatusCode,new
                    {
                        message = result.Message,
                        errors = result.Errors
                    });
                }
                return StatusCode(result.StatusCode,new
                {
                    message = result.Message
                });

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPost("login")]
        public async Task<ActionResult> Login(LoginDto loginDto)
        {
            try
            {
                var result = await _authService.Login(loginDto);
                if (!result.IsSuccess)
                {
                    return StatusCode(result.StatusCode, new
                    {
                        message = result.Message,
                        errors = result.Errors
                    });
                }
                return StatusCode(result.StatusCode,new
                {
                    message = result.Message,
                    token=result.Data
                });
            }
            catch (Exception er)
            {
                return BadRequest(er.Message);
            }
        }
    }
}
