using Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XFM.BLL.Result;

namespace XFM.BLL.Services.AuthService
{
    public interface IAuthService
    {
        Task<ResultViewModel<string>> Register(RegisterDto registerDto);
        Task<ResultViewModel<string>> Login(LoginDto loginDto);
    }
}
