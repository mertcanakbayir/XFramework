using Microsoft.AspNetCore.Mvc;
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
        public async Task<ActionResult> GetAll()
        {
            var result= await _userService.GetUsers();
            if(!result.IsSuccess)
            {
                return StatusCode(result.StatusCode, new
                {
                    message=result.Message,
                    errors=result.Errors
                });
            }
            return StatusCode(result.StatusCode, new {
            data=result.Data,
            message=result.Message
            });
        }
    }
}

