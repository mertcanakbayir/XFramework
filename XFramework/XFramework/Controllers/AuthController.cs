using Dtos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using XFM.BLL.Services.UserService;

namespace XFramework.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly  IHashService _hashService;
        public AuthController(IHashService hashService)
        {
            _hashService = hashService;
        }

        
    }
}
