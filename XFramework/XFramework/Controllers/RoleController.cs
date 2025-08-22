using Microsoft.AspNetCore.Mvc;
using XFramework.BLL.Result;
using XFramework.BLL.Services.Concretes;
using XFramework.Dtos;

namespace XFramework.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoleController : ControllerBase
    {
        private readonly RoleService _roleService;

        public RoleController(RoleService roleService)
        {
            _roleService = roleService;
        }
        [HttpGet]
        public async Task<ResultViewModel<List<RoleDto>>> GetUserRoles(int userId)
        {
            return await _roleService.GetRolesByUser(userId);
        }

        [HttpPost]
        public async Task<ResultViewModel<string>> AddUserRole(RoleAddDto addRole)
        {
            int userId = int.Parse(User.FindFirst("sub").Value);
            return await _roleService.AddRole(addRole, userId);
        }

        [HttpPost("addPageRole")]
        public async Task<ResultViewModel<string>> AddPageRole(PageRoleAddDto pageRoleAddDto)
        {
            int userId = int.Parse(User.FindFirst("sub").Value);
            return await _roleService.AddPageRole(pageRoleAddDto, userId);
        }

        [HttpPost("addEndpointRole")]
        public async Task<ResultViewModel<string>> AddEndpointRole(EndpointRoleAddDto endpointRoleAddDto)
        {
            int userId = int.Parse(User.FindFirst("sub").Value);
            return await _roleService.AddEndpointRole(endpointRoleAddDto, userId);
        }

    }
}
