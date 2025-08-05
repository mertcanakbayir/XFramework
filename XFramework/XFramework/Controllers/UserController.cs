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

    }
}

