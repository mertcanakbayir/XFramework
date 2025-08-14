using Dtos;
using Microsoft.AspNetCore.Mvc;
using XFM.BLL.Result;
using XFM.BLL.Services.UserService;
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
        [ValidateFilter]
        public async Task<ResultViewModel<List<UserDto>>> GetAll()
        {
           return await _userService.GetUsers();
        }
    }
}

