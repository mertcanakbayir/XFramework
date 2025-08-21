using Dtos;
using XFramework.BLL.Result;
namespace XFramework.BLL.Services.Abstracts
{
    public interface IUserService
    {
        Task<ResultViewModel<UserDto>> GetUserById(int id);
        Task<ResultViewModel<UserDto>> GetUserByEmail(string email);

        Task<ResultViewModel<List<UserDto>>> GetUsers();
    }
}
