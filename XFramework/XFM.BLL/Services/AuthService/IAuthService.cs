using Dtos;
using XFM.BLL.Result;

namespace XFM.BLL.Services.AuthService
{
    public interface IAuthService
    {
        Task<ResultViewModel<string>> Register(RegisterDto registerDto);
        Task<ResultViewModel<string>> Login(LoginDto loginDto);
    }
}
