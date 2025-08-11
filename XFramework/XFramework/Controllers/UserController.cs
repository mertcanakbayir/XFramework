using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using XFM.BLL.Services.UserService;
using XFM.DAL.Abstract;
using XFM.DAL.Entities;
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


        [HttpGet("get-all-users")]
        [Authorize(Policy ="AdminOnly")]
        public async Task<ActionResult> Get() {
            var result= await _userService.GetUsers();
            if (!result.IsSuccess)
            {
                return StatusCode(result.StatusCode,new {
                    message=result.Message,
                    errors= result.Errors } );
            }
            return StatusCode(result.StatusCode, new
            {
                message = result.Message,
                data = result.Data
            });
        }

    }
}

