using Dtos;
using XFM.BLL.Result;
using XFramework.Dtos;

namespace XFM.BLL.Services.AuthService
{
    public interface IAuthService
    {
        Task<ResultViewModel<string>> Register(RegisterDto registerDto);
        Task<ResultViewModel<string>> Login(LoginDto loginDto);

        Task<ResultViewModel<PasswordResetTokenDto>> ForgotPasswordAsync(ForgotPasswordDto forgotPasswordDto);

        Task<ResultViewModel<string>> ResetPasswordAsync(ResetPasswordDto resetPasswordDto);

    }
}
