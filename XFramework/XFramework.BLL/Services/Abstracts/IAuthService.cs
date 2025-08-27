using Dtos;
using XFramework.Dtos;
using XFramework.Helper.ViewModels;

namespace XFramework.BLL.Services.Abstracts
{
    public interface IAuthService
    {
        Task<ResultViewModel<string>> Register(RegisterDto registerDto);
        Task<ResultViewModel<string>> Login(LoginDto loginDto);

        Task<ResultViewModel<PasswordResetTokenDto>> ForgotPasswordAsync(ForgotPasswordDto forgotPasswordDto);

        Task<ResultViewModel<string>> ResetPasswordAsync(ResetPasswordDto resetPasswordDto);

    }
}
