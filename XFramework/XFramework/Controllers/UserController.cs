using Dtos;
using Microsoft.AspNetCore.Mvc;
using XFramework.BLL.Result;
using XFramework.BLL.Services.Abstracts;
namespace XFramework.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        [TypeFilter(typeof(ValidateFilter))]
        public async Task<ResultViewModel<List<UserDto>>> GetAll()
        {
            return await _userService.GetUsers();
        }
    }
}

