using Dtos;
using Microsoft.AspNetCore.Mvc;
using XFramework.BLL.Result;
using XFramework.BLL.Services.Abstracts;
using XFramework.BLL.Services.Concretes;
using XFramework.Dtos;
namespace XFramework.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly UserService _userService;
        public UserController(UserService userService)
        {
            _userService = userService;
        }
        [HttpGet("all")]
        [TypeFilter(typeof(ValidateFilter))]
        public async Task<ResultViewModel<List<UserDto>>> GetAllUsers()
        {
            return await _userService.GetUsers();
        }

        [HttpPost]
        [TypeFilter(typeof(ValidateFilter))]
        public async Task<ResultViewModel<UserAddDto>> AddUser(UserAddDto userAddDto)
        {
            return await _userService.AddUser(userAddDto);
        }

        [HttpPut]
        [TypeFilter(typeof(ValidateFilter))]
        public async Task<ResultViewModel<UserUpdateDto>> UpdateUser(UserUpdateDto userUpdateDto)
        {
            return await _userService.UpdateUser(userUpdateDto);
        }

        [HttpGet]
        [TypeFilter(typeof(ValidateFilter))]
        public async Task<ResultViewModel<UserDto>> GetUserById(int userId)
        {
            return await _userService.GetUserById(userId);
        }

        [HttpDelete]
        [TypeFilter(typeof(ValidateFilter))]
        public async Task<ResultViewModel<string>> DeleteUserById(int userId)
        {
            return await _userService.DeleteUserById(userId);
        }
    }
}

