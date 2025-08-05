using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dtos;
using XFM.BLL.Result;
namespace XFM.BLL.Services.UserService
{
    public interface IUserService
    {
        Task<Result<UserDto>> GetUserById(int id);
        Task<Result<UserDto>> GetUserByEmail(string email);
    }
}
