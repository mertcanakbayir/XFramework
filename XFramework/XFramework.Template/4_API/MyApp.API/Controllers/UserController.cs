using Microsoft.AspNetCore.Mvc;
using MyApp.BLL.Services.Concretes;
using MyApp.Dtos.User;
using MyApp.Helper.ViewModels;
namespace MyApp.Controllers
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
        public async Task<PagedResultViewModel<UserDto>> GetAllUsers()
        {
            return await _userService.GetAllAsync();
        }

        [HttpPost]
        [TypeFilter(typeof(ValidateFilter))]
        public async Task<ResultViewModel<UserAddDto>> AddUser(UserAddDto userAddDto)
        {
            return await _userService.AddUser(userAddDto);
        }

        [HttpPut("{id}")]
        [TypeFilter(typeof(ValidateFilter))]
        public async Task<ResultViewModel<UserUpdateDto>> UpdateUser(UserUpdateDto userUpdateDto, int id)
        {
            return await _userService.UpdateUser(id, userUpdateDto);
        }

        [HttpGet]
        [TypeFilter(typeof(ValidateFilter))]
        public async Task<ResultViewModel<UserDto>> GetUserById(int userId)
        {
            return await _userService.GetAsync(e => e.Id == userId);
        }

        [HttpDelete]
        [TypeFilter(typeof(ValidateFilter))]
        public async Task<ResultViewModel<string>> DeleteUserById(int userId)
        {
            return await _userService.DeleteAsync(userId);
        }
    }
}

